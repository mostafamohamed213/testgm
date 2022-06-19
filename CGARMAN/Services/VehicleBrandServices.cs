using CGARMAN.ViewModel;
using CGARMAN.ViewModel.Shared;
using CGARMAN.ViewModel.Vehicle;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class VehicleBrandServices
    {
        IUnitOfWork unitOfWork;

        public VehicleBrandServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public PagingViewModel<VehicleBrand> getAllVehicleBrandsPaging(int currentPage)
        {
            PagingViewModel<VehicleBrand> model = new PagingViewModel<VehicleBrand>();
            var VehicleBrands = unitOfWork.VehicleBrand.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.VehicleBrandIndex, TablesMaxRows.VehicleBrandIndex, d => d.VehicleBrandId, OrderBy.Ascending,new []{ "VehicleFamily" });
            model.items = VehicleBrands.ToList();
            var itemsCount = unitOfWork.VehicleBrand.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.VehicleBrandIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.VehicleBrandIndex;
            return model;
        }
      
        internal SelectList GetAllFamilies(int VehicleFamilyId = 0)
        {
            var Families = unitOfWork.VehicleFamily.GetAllWithCriteria(c => c.Enable);
            if (Families.Count() > 0)
            {
                if (VehicleFamilyId > 0)
                {
                    return new SelectList(Families, "VehicleFamilyId", "Name", VehicleFamilyId);
                }
                return new SelectList(Families, "VehicleFamilyId", "Name");
            }
            return null;
        }
        public PagingViewModel<VehicleBrand> getAllVehicleBrandsPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.VehicleBrandIndex = length;
            return getAllVehicleBrandsPaging(currentPageIndex);
        }

        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.VehicleBrand.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleBrandId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.VehicleBrand.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleBrandId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal void Create(VehicleBrandCreateViewModel model)
        {
            VehicleBrand VehicleBrand = new VehicleBrand()
            {
                Name = model.Name.Trim(),
                CreateDts = DateTime.Now,
                Enable = true,               
                SystemUserCreate = "1",
                VehicleFamilyId =model.VehicleFamilyId                
            };
            unitOfWork.VehicleBrand.Add(VehicleBrand);
            unitOfWork.Complete();
        }

        internal bool SearchIfNameExists(string name,int VehicleFamilyId  ,int id = 0)
        {
            return unitOfWork.VehicleBrand.GetOne(c => c.Enable && c.Name.ToLower().Trim() == name.ToLower().Trim()&& c.VehicleFamilyId == VehicleFamilyId && c.VehicleBrandId != id) == null ? true : false;
        }

        internal VehicleBrand getVehicleBrandById(int id)
        {
            return unitOfWork.VehicleBrand.GetOne(c => c.Enable && c.VehicleBrandId == id,new[] { "VehicleFamily" });
        }

        internal PagingViewModel<VehicleBrand> Search(string Name, int currentPage, int FamilyId)
        {
            List<VehicleBrand> VehicleBrands =new List<VehicleBrand>();
            if (!String.IsNullOrEmpty(Name)&& FamilyId > 0)
            {
                VehicleBrands = unitOfWork.VehicleBrand.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().Trim().Contains(Name.ToLower().Trim()) &&  c.VehicleFamilyId == FamilyId , new[] { "VehicleFamily" }).ToList();

            }
            else if (!String.IsNullOrEmpty(Name) && FamilyId < 1)
            {
                VehicleBrands = unitOfWork.VehicleBrand.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().Trim().Contains(Name.ToLower().Trim()), new[] { "VehicleFamily" }).ToList();

            }
            else if (String.IsNullOrEmpty(Name) && FamilyId > 0)
            {
                VehicleBrands = unitOfWork.VehicleBrand.GetAllWithCriteria(c => c.Enable && c.VehicleFamilyId == FamilyId , new[] {"VehicleFamily"}).ToList();

            }
            PagingViewModel<VehicleBrand> model = new PagingViewModel<VehicleBrand>();
            model.items = VehicleBrands;
            var itemsCount = VehicleBrands.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        internal void Edit(VehicleBrandEditViewModel model)
        {
            var VehicleBrand = getVehicleBrandById(model.VehicleBrandId);
            VehicleBrand.Name = model.Name.Trim();
            VehicleBrand.VehicleFamilyId = model.VehicleFamilyId;            
            unitOfWork.Complete();
        }

        internal void Delete(int VehicleBrandId)
        {
            var VehicleBrand = getVehicleBrandById(VehicleBrandId);
            VehicleBrand.Enable = false;
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
                    List<VehicleBrand> BrandList = new List<VehicleBrand>();
                    List<string> existList = new List<string>();

                    DateTime now = DateTime.Now;
                    Dictionary<string, int> Families = unitOfWork.VehicleFamily.GetAllWithCriteria(c=>c.Enable).ToDictionary(i => i.Name, i => i.VehicleFamilyId);
                    List<string> existBrands = unitOfWork.VehicleBrand.GetAll().Select(c => c.Name.ToLower()).ToList();

                    int rowNumber = 1;
                    while (reder.Read())
                    {
                        if (rowNumber > 1)// ignore header
                        {
                            existList = BrandList.Select(c => c.Name.ToLower()).ToList();
                            //validation
                            if (reder.GetValue(0) == null || string.IsNullOrWhiteSpace(reder.GetValue(0).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name." };
                            if (existBrands.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (This is Brand already exist)." };
                            if (existList.Contains(reder.GetValue(0).ToString().ToLower().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name (duplicate values)" };
                            if (reder.GetValue(1) == null || string.IsNullOrWhiteSpace(reder.GetValue(1).ToString()) || !Families.ContainsKey(reder.GetValue(1).ToString().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Family." };

                        
                            BrandList.Add(new VehicleBrand
                            {
                                Name = reder.GetValue(0).ToString().Trim(),
                                VehicleFamilyId = Families[reder.GetValue(1).ToString().Trim()],
                                CreateDts = DateTime.Now,
                                Enable = true,
                                SystemUserCreate = "1",
                            });

                        }
                        rowNumber++;
                    }
                
                    unitOfWork.VehicleBrand.AddRange(BrandList);
                    unitOfWork.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is IndexOutOfRangeException )
                    {
                        return new ImportFileStatus { status = 0, message = $"File format is not valid" };
                    }                   
                    return new ImportFileStatus { status = 0, message = $"Error : {ex.Message}" };
                }
            }
            return new ImportFileStatus { status = 1, message = "Successful" };
        }
        internal List<VehicleFamily> GetVehicleFamiliesForExportToExcel()
        {
            List<VehicleFamily> model = new List<VehicleFamily>();
            model.Add(new VehicleFamily { Name = "Name", });
            model.AddRange(unitOfWork.VehicleFamily.GetAllWithCriteria(c => c.Enable).ToList());
            return model;
        }
    }
}
