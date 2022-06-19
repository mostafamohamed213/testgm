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
    public class VehicleOwnerServices
    {
        IUnitOfWork unitOfWork;
        public VehicleOwnerServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public PagingViewModel<VehicleOwner> getAllVehicleOwnerPaging(int currentPage)
        {
            PagingViewModel<VehicleOwner> model = new PagingViewModel<VehicleOwner>();
            var VehicleOwners = unitOfWork.VehicleOwner.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.VehicleOwnerIndex, TablesMaxRows.VehicleOwnerIndex, d => d.VehicleOwnerId, OrderBy.Ascending);
            model.items = VehicleOwners.ToList();
            var itemsCount = unitOfWork.VehicleOwner.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.VehicleOwnerIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.VehicleOwnerIndex;
            return model;
        }
        public PagingViewModel<VehicleOwner> getAllVehicleOwnerPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.VehicleOwnerIndex = length;
            return getAllVehicleOwnerPaging(currentPageIndex);
        }

        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.VehicleOwner.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleOwnerId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.VehicleOwner.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleOwnerId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal void Create(string name)
        {
            VehicleOwner VehicleOwner = new VehicleOwner()
            {
                Name = name.Trim(),
                Enable = true,
                SystemUserCreate = "1",
                CreateDts = DateTime.Now
            };
            unitOfWork.VehicleOwner.Add(VehicleOwner);
            unitOfWork.Complete();
        }

        internal bool SearchIfNameExists(string name, int id = 0)
        {
            return unitOfWork.VehicleOwner.GetOne(c => c.Enable && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.VehicleOwnerId != id) == null ? true : false;
        }

        internal VehicleOwner getVehicleOwnerById(int id)
        {
            return unitOfWork.VehicleOwner.GetOne(c => c.Enable && c.VehicleOwnerId == id);
        }

        internal PagingViewModel<VehicleOwner> Search(string Name, int currentPage)
        {
            var VehicleOwners = unitOfWork.VehicleOwner.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            PagingViewModel<VehicleOwner> model = new PagingViewModel<VehicleOwner>();
            model.items = VehicleOwners.ToList();
            var itemsCount = VehicleOwners.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        internal void Edit(VehicleOwnerEditViewModel model)
        {
            var VehicleOwner = getVehicleOwnerById(model.VehicleOwnerId);
            VehicleOwner.Name = model.Name.Trim();
            unitOfWork.Complete();
        }

        internal void Delete(int VehicleOwnerId)
        {
            var VehicleOwner = getVehicleOwnerById(VehicleOwnerId);
            VehicleOwner.Enable = false;
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

                    List<VehicleOwner> owners = new List<VehicleOwner>();
                    List<string> existList = new List<string>();
                    List<string> existOwners = unitOfWork.VehicleOwner.GetAllWithCriteria(c=>c.Enable).Select(c => c.Name.ToLower()).ToList();
                    DateTime now = DateTime.Now;

                    int rowNumber = 1;
                    while (reder.Read())
                    {
                        if (rowNumber > 1)// ignore header
                        {
                            existList = owners.Select(c => c.Name.ToLower()).ToList();
                            //validation
                            if (reder.GetValue(0) == null || string.IsNullOrWhiteSpace(reder.GetValue(0).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name." };
                            if (existOwners.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (This is owner already exist)." };
                            if (existList.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (duplicate values)" };


                            owners.Add(new VehicleOwner
                            {
                                Name = reder.GetValue(0).ToString().Trim(),
                                CreateDts = now,
                                Enable = true,
                                SystemUserCreate = "1",
                            });
                        }
                        rowNumber++;
                    }
               
                    unitOfWork.VehicleOwner.AddRange(owners);
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
