using CGARMAN.ViewModel;
using CGARMAN.ViewModel.TechnicianViewModels;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Services
{
    public class ShiftServices
    {
        IUnitOfWork unitOfWork;
        public ShiftServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public PagingViewModel<Shift> getAllShiftsPaging(int currentPage)
        {
            PagingViewModel<Shift> model = new PagingViewModel<Shift>();
            var Shifts = unitOfWork.Shift.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.ShiftIndex, TablesMaxRows.ShiftIndex, d => d.ShiftId, OrderBy.Ascending);
            model.items = Shifts.ToList();
            var itemsCount = unitOfWork.Shift.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.ShiftIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.ShiftIndex;
            return model;
        }
        public PagingViewModel<Shift> getAllShiftsPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.TechnicianIndex = length;
            return getAllShiftsPaging(currentPageIndex);
        }

        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.Shift.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.ShiftId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.Shift.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.ShiftId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal void Create(string name)
        {
            Shift Shift = new Shift()
            {
                Name = name.Trim(),              
                Enable = true,
            };
            unitOfWork.Shift.Add(Shift);
            unitOfWork.Complete();
        }

        internal bool SearchIfNameExists(string name, int id = 0)
        {
            return unitOfWork.Shift.GetOne(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.ShiftId != id) == null ? true : false;
        }

        internal Shift getShiftById(int id)
        {
            return unitOfWork.Shift.GetOne(c => c.ShiftId == id);
        }

        internal PagingViewModel<Shift> Search(string Name, int currentPage)
        {
            var Shifts = unitOfWork.Shift.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            PagingViewModel<Shift> model = new PagingViewModel<Shift>();
            model.items = Shifts.ToList();
            var itemsCount = Shifts.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        internal void Edit(EditShiftViewModel model)
        {
            var Shfit = getShiftById(model.TechnicianShiftId);
            Shfit.Name = model.Name.Trim();
            unitOfWork.Complete();
        }

        internal void Delete(int technicianShiftId)
        {
            var shift = getShiftById(technicianShiftId);
            shift.Enable = false;
            unitOfWork.Complete();
        }
    }
}

