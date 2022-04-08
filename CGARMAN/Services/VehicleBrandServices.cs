using CGARMAN.ViewModel;
using CGARMAN.ViewModel.Vehicle;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
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
            return unitOfWork.VehicleBrand.GetOne(c => c.Name.ToLower().Trim() == name.ToLower().Trim()&& c.VehicleFamilyId == VehicleFamilyId && c.VehicleBrandId != id) == null ? true : false;
        }

        internal VehicleBrand getVehicleBrandById(int id)
        {
            return unitOfWork.VehicleBrand.GetOne(c => c.VehicleBrandId == id,new[] { "VehicleFamily" });
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
    }
}
