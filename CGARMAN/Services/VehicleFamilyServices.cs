using CGARMAN.ViewModel;
using CGARMAN.ViewModel.Vehicle;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
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
            return unitOfWork.VehicleFamily.GetOne(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.VehicleFamilyId != id) == null ? true : false;
        }

        internal VehicleFamily getVehicleFamilyById(int id)
        {
            return unitOfWork.VehicleFamily.GetOne(c => c.VehicleFamilyId == id);
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
    }
}
