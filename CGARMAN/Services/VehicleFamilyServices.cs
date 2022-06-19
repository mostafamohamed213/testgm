using CGARMAN.ViewModel;
using CGARMAN.ViewModel.Shared;
using CGARMAN.ViewModel.Vehicle;
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
    public class VehicleFamilyServices
    {
        IUnitOfWork unitOfWork;
        public VehicleFamilyServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
        public PagingViewModel<VehicleFamily> getAllVehicleFamilyPaging(int currentPage)
        {
            PagingViewModel<VehicleFamily> model = new PagingViewModel<VehicleFamily>();
            var Families = unitOfWork.VehicleFamily.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.VehicleFamilyIndex, TablesMaxRows.VehicleFamilyIndex, d => d.VehicleFamilyId, OrderBy.Ascending);
            model.items = Families.ToList();
            var itemsCount = unitOfWork.VehicleFamily.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.VehicleFamilyIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.VehicleFamilyIndex;
            return model;
        }
        public PagingViewModel<VehicleFamily> getAllVehicleFamilyPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.VehicleFamilyIndex = length;
            return getAllVehicleFamilyPaging(currentPageIndex);
        }

        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.VehicleFamily.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleFamilyId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.VehicleFamily.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleFamilyId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal void Create(string name)
        {
            VehicleFamily VehicleFamily = new VehicleFamily()
            {
                Name = name.Trim(),
                Enable = true,
                SystemUserCreate = "1",
                CreateDts = DateTime.Now,                
            };
            unitOfWork.VehicleFamily.Add(VehicleFamily);
            unitOfWork.Complete();
        }

        internal bool SearchIfNameExists(string name, int id = 0)
        {
            return unitOfWork.VehicleFamily.GetOne(c => c.Enable && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.VehicleFamilyId != id) == null ? true : false;
        }

        internal VehicleFamily getVehicleFamilyById(int id)
        {
            return unitOfWork.VehicleFamily.GetOne(c => c.Enable && c.VehicleFamilyId == id);
        }

        internal PagingViewModel<VehicleFamily> Search(string Name, int currentPage)
        {
            var Families = unitOfWork.VehicleFamily.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            PagingViewModel<VehicleFamily> model = new PagingViewModel<VehicleFamily>();
            model.items = Families.ToList();
            var itemsCount = Families.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        internal void Edit(VehicleFamilyEditViewModel model)
        {
            var VehicleFamily = getVehicleFamilyById(model.VehicleFamilyId);
            VehicleFamily.Name = model.Name.Trim();
            unitOfWork.Complete();
        }

        internal void Delete(int VehicleFamilyId)
        {
            var VehicleFamily = getVehicleFamilyById(VehicleFamilyId);
            VehicleFamily.Enable = false;
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

                    List<VehicleFamily> families = new List<VehicleFamily>();

                    List<string> existFamilies = unitOfWork.VehicleFamily.GetAll().Select(c => c.Name.ToLower()).ToList();
                    List<string> existList = new List<string>();

                    DateTime now = DateTime.Now;

                    int rowNumber = 1;
                    while (reder.Read())
                    {
                        if (rowNumber > 1)// ignore header
                        {
                            existList = families.Select(c => c.Name.ToLower()).ToList();
                            //validation
                            if (reder.GetValue(0) == null || string.IsNullOrWhiteSpace(reder.GetValue(0).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name." };
                            if (existFamilies.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (This is Family already exist)." };
                            if (existList.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (duplicate values)" };

                            families.Add(new VehicleFamily
                            {
                                Name = reder.GetValue(0).ToString().Trim(),
                                CreateDts = now,
                                Enable = true,
                                SystemUserCreate = "1",
                            });
                        
                        }
                        rowNumber++;
                    }
                
                    unitOfWork.VehicleFamily.AddRange(families);
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
