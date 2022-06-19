using CGARMAN.ViewModel;
using CGARMAN.ViewModel.Shared;
using CGARMAN.ViewModel.TechnicianViewModels;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Services
{
    public class PositionServices
    {
        IUnitOfWork unitOfWork;
        public PositionServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public PagingViewModel<TechnicianPosition> getAllPositionsPaging(int currentPage)
        {
            PagingViewModel<TechnicianPosition> model = new PagingViewModel<TechnicianPosition>();
            var Positions = unitOfWork.TechnicianPosition.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.PositionIndex, TablesMaxRows.PositionIndex, d => d.TechnicianPositionId, OrderBy.Ascending);
            model.items = Positions.ToList();
            var itemsCount = unitOfWork.TechnicianPosition.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.PositionIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.PositionIndex;
            return model;
        }
        public PagingViewModel<TechnicianPosition> getAllPositionsPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.PositionIndex = length;
            return getAllPositionsPaging(currentPageIndex);
        }
        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.TechnicianPosition.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.TechnicianPositionId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.TechnicianPosition.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.TechnicianPositionId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }
        internal PagingViewModel<TechnicianPosition> Search(string Name, int currentPage)
        {
            var Positions = unitOfWork.TechnicianPosition.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            PagingViewModel<TechnicianPosition> model = new PagingViewModel<TechnicianPosition>();
            model.items = Positions.ToList();
            var itemsCount = Positions.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }
        internal void Create(string name)
        {
            TechnicianPosition Technician = new TechnicianPosition()
            {
                Name = name.Trim(),
                CreateDts = DateTime.Now,
                Enable = true,
                SystemUserID="1",                
            };
            unitOfWork.TechnicianPosition.Add(Technician);
            unitOfWork.Complete();
        }
        internal bool SearchIfNameExists(string name, int id = 0)
        {
            return unitOfWork.TechnicianPosition.GetOne(c => c.Enable && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.TechnicianPositionId != id) == null ? true : false;
        }

        internal TechnicianPosition getPositionById(int id)
        {
            return unitOfWork.TechnicianPosition.GetOne(c => c.Enable && c.TechnicianPositionId == id);
        }
        internal void Edit(EditPositionViewModel model)
        {
            var position = getPositionById(model.TechnicianPositionId);
            position.Name = model.Name.Trim();
            unitOfWork.Complete();
        }

        internal void Delete(int technicianPositionId)
        {
            var position = getPositionById(technicianPositionId);
            position.Enable = false;
            unitOfWork.Complete();
        }
        internal ImportFileStatus GetDataFromCSVFile(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reder = ExcelReaderFactory.CreateReader(stream))
            {
                try
                {
                    if (reder.RowCount <= 1)
                    {
                        return new ImportFileStatus { status = 0, message = $"This file is empty." };
                    }

                    List<TechnicianPosition> positions = new List<TechnicianPosition>();

                    List<string> existPositions = unitOfWork.TechnicianPosition.GetAll().Select(c => c.Name.ToLower()).ToList();
                    List<string> existList = new List<string>();

                    DateTime now = DateTime.Now;

                    int rowNumber = 1;
                    while (reder.Read())
                    {
                        if (rowNumber > 1)// ignore header
                        {
                            existList = positions.Select(c => c.Name.ToLower()).ToList();

                            //validation
                            if (reder.GetValue(0) == null || string.IsNullOrWhiteSpace(reder.GetValue(0).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name." };
                            if (existPositions.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (This is position already exist)." };
                            if (existList.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (duplicate values)" };

                            positions.Add(new TechnicianPosition
                            {
                                Name = reder.GetValue(0).ToString().Trim(),
                                CreateDts = now,
                                Enable = true,
                                SystemUserID = "1",
                            });
                        }
                        rowNumber++;
                    }
               
                    unitOfWork.TechnicianPosition.AddRange(positions);
                    unitOfWork.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is IndexOutOfRangeException)
                    {
                        return new ImportFileStatus { status = 0, message = $"File format is not valid" };
                    }
                    return new ImportFileStatus { status = 0, message = $"Error : {ex.Message}" };
                }
            }
            return new ImportFileStatus { status = 1, message = "Successful" };
        }

    }
}
