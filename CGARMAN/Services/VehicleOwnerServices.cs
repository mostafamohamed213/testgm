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
            return unitOfWork.VehicleOwner.GetOne(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.VehicleOwnerId != id) == null ? true : false;
        }

        internal VehicleOwner getVehicleOwnerById(int id)
        {
            return unitOfWork.VehicleOwner.GetOne(c => c.VehicleOwnerId == id);
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
    }
}
