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
    public class VehicleDepartmentServices
    {
        IUnitOfWork unitOfWork;
        public VehicleDepartmentServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public PagingViewModel<VehicleDepartment> getAllVehicleDepartmentsPaging(int currentPage)
        {
            PagingViewModel<VehicleDepartment> model = new PagingViewModel<VehicleDepartment>();
            var VehicleDepartments = unitOfWork.VehicleDepartment.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.VehicleDepartmentIndex, TablesMaxRows.VehicleDepartmentIndex, d => d.VehicleDepartmentId, OrderBy.Ascending);
            model.items = VehicleDepartments.ToList();
            var itemsCount = unitOfWork.VehicleDepartment.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.VehicleDepartmentIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.VehicleDepartmentIndex;
            return model;
        }
        public PagingViewModel<VehicleDepartment> getAllVehicleDepartmentsPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.VehicleDepartmentIndex = length;
            return getAllVehicleDepartmentsPaging(currentPageIndex);
        }

        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.VehicleDepartment.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleDepartmentId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.VehicleDepartment.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleDepartmentId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal void Create(string name)
        {
            VehicleDepartment VehicleDepartment = new VehicleDepartment()
            {
                Name = name.Trim(),
                Enable = true,
                SystemUserCreate = "1",
                CreateDts = DateTime.Now
            };
            unitOfWork.VehicleDepartment.Add(VehicleDepartment);
            unitOfWork.Complete();
        }

        internal bool SearchIfNameExists(string name, int id = 0)
        {
            return unitOfWork.VehicleDepartment.GetOne(c => c.Enable && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.VehicleDepartmentId != id) == null ? true : false;
        }

        internal VehicleDepartment getVehicleDepartmentById(int id)
        {
            return unitOfWork.VehicleDepartment.GetOne(c => c.Enable && c.VehicleDepartmentId == id);
        }

        internal PagingViewModel<VehicleDepartment> Search(string Name, int currentPage)
        {
            var VehicleDepartments = unitOfWork.VehicleDepartment.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            PagingViewModel<VehicleDepartment> model = new PagingViewModel<VehicleDepartment>();
            model.items = VehicleDepartments.ToList();
            var itemsCount = VehicleDepartments.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        internal void Edit(VehicleDepartmentEditViewModel model)
        {
            var VehicleDepartment = getVehicleDepartmentById(model.VehicleDepartmentId);
            VehicleDepartment.Name = model.Name.Trim();
            unitOfWork.Complete();
        }

        internal void Delete(int VehicleDepartmentId)
        {
            var VehicleDepartment = getVehicleDepartmentById(VehicleDepartmentId);
            VehicleDepartment.Enable = false;
            unitOfWork.Complete();
        }
    }
}
