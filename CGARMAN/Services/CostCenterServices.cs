using CGARMAN.ViewModel;
using CGARMAN.ViewModel.Shared;
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
    public class CostCenterServices
    {
        IUnitOfWork unitOfWork;
        
        public CostCenterServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public PagingViewModel<CostCenter> getAllCostCentersPaging(int currentPage)
        {
            PagingViewModel<CostCenter> model = new PagingViewModel<CostCenter>();
            var costCenters = unitOfWork.CostCenters.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.CostCenterIndex,TablesMaxRows.CostCenterIndex, d => d.CostCenterId, OrderBy.Ascending);
            model.items = costCenters.ToList();
            var itemsCount = unitOfWork.CostCenters.Count(c=>c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.CostCenterIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength =TablesMaxRows.CostCenterIndex;
            return model;
        }
        public PagingViewModel<CostCenter> getAllCostCentersPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.CostCenterIndex = length;
            return getAllCostCentersPaging(currentPageIndex);
        }

        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.CostCenters.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.CostCenterId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.CostCenters.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.CostCenterId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal void Create(CostCenterCreateViewModel model)
        {
            CostCenter costCenter = new CostCenter()
            {
                Name = model.Name.Trim(),
                CreateDts = DateTime.Now,
                Enable = true,
                Value = model.Value.Trim(),
                Systemusercrate = "1"                
            };
            unitOfWork.CostCenters.Add(costCenter);
            unitOfWork.Complete();
        }

        internal bool SearchIfNameExists(string name, int id = 0)
        {
            return unitOfWork.CostCenters.GetOne(c => c.Enable && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.CostCenterId != id) == null ? true : false;
        }

        internal CostCenter getCostCenterById(int id)
        {
            return unitOfWork.CostCenters.GetOne(c => c.Enable && c.CostCenterId == id);
        }

        internal PagingViewModel<CostCenter> Search(string Name, int currentPage)
        {
            var CostCenters = unitOfWork.CostCenters.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            PagingViewModel<CostCenter> model = new PagingViewModel<CostCenter>();
            model.items = CostCenters.ToList();
            var itemsCount = CostCenters.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        internal void Edit(CostCenterEditViewModel model)
        {
            var CostCenter = getCostCenterById(model.CostCenterId);
            CostCenter.Name = model.Name.Trim();
            CostCenter.Value = model.Value.Trim();
            unitOfWork.Complete();
        }

        internal void Delete(int CostCenterId)
        {
            var CostCenter = getCostCenterById(CostCenterId);
            CostCenter.Enable = false;
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

                    List<CostCenter> costCenters = new List<CostCenter>();

                    List<string> existCostCenters = unitOfWork.CostCenters.GetAllWithCriteria(c => c.Enable).Select(c => c.Name.ToLower()).ToList();
                    List<string> existList = new List<string>();
                    DateTime now = DateTime.Now;

                    int rowNumber = 1;
                    while (reder.Read())
                    {
                        if (rowNumber > 1)// ignore header
                        {
                            existList = costCenters.Select(c => c.Name.ToLower()).ToList();
                            //validation
                            if (reder.GetValue(0) == null || string.IsNullOrWhiteSpace(reder.GetValue(0).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name." };
                            if (existCostCenters.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (This is Tire Size already exist)." };
                            if (existList.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (duplicate values)" };
                            if (reder.GetValue(1) == null || string.IsNullOrWhiteSpace(reder.GetValue(1).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Value." };

                            costCenters.Add(new CostCenter
                            {
                                Name = reder.GetValue(0).ToString().Trim(),
                                Value = reder.GetValue(1).ToString().Trim(),
                                CreateDts = now,
                                Enable = true,
                                Systemusercrate = "1",
                            });
                        }
                        rowNumber++;
                    }
                
                    unitOfWork.CostCenters.AddRange(costCenters);
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
