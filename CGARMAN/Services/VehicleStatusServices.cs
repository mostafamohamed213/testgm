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
    public class VehicleStatusServices
    {
        IUnitOfWork unitOfWork;
        public VehicleStatusServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
        public PagingViewModel<VehicleStatus> getAllVehicleStatusPaging(int currentPage)
        {
            PagingViewModel<VehicleStatus> model = new PagingViewModel<VehicleStatus>();
            var vehicleStatus = unitOfWork.VehicleStatus.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.VehicleStatusIndex, TablesMaxRows.VehicleStatusIndex, d => d.VehicleStatusId, OrderBy.Ascending);
            model.items = vehicleStatus.ToList();
            var itemsCount = unitOfWork.VehicleStatus.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.VehicleStatusIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.VehicleStatusIndex;
            return model;
        }
        public PagingViewModel<VehicleStatus> getAllVehicleStatusPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.VehicleStatusIndex = length;
            return getAllVehicleStatusPaging(currentPageIndex);
        }

        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.VehicleStatus.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleStatusId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.VehicleStatus.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleStatusId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal void Create(string name)
        {
            VehicleStatus VehicleStatus = new VehicleStatus()
            {
                Name = name.Trim(),
                Enable = true,
                SystemUserCreate = "1",
                CreateDts = DateTime.Now
            };
            unitOfWork.VehicleStatus.Add(VehicleStatus);
            unitOfWork.Complete();
        }

        internal bool SearchIfNameExists(string name, int id = 0)
        {
            return unitOfWork.VehicleStatus.GetOne(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.VehicleStatusId != id) == null ? true : false;
        }

        internal VehicleStatus getVehicleStatusById(int id)
        {
            return unitOfWork.VehicleStatus.GetOne(c => c.VehicleStatusId == id);
        }

        internal PagingViewModel<VehicleStatus> Search(string Name, int currentPage)
        {
            var vehicleStatus = unitOfWork.VehicleStatus.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            PagingViewModel<VehicleStatus> model = new PagingViewModel<VehicleStatus>();
            model.items = vehicleStatus.ToList();
            var itemsCount = vehicleStatus.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        internal void Edit(VehicleStatusEditViewModel model)
        {
            var VehicleStatus = getVehicleStatusById(model.VehicleStatusId);
            VehicleStatus.Name = model.Name.Trim();
            unitOfWork.Complete();
        }

        internal void Delete(int VehicleStatusId)
        {
            var VehicleStatus = getVehicleStatusById(VehicleStatusId);
            VehicleStatus.Enable = false;
            unitOfWork.Complete();
        }
    }
}
