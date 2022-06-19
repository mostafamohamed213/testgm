using CGARMAN.ViewModel;
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
    public class SchedulesServices
    {
        IUnitOfWork unitOfWork;
        public SchedulesServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public PagingViewModel<Schedule> getAllSchedulesPaging(int currentPage,long VehicleId = 0,DateTime? dateSchedules=null)
        {
            PagingViewModel<Schedule> model = new PagingViewModel<Schedule>();
            var Schedules = unitOfWork.Schedule.FindAll(c => c.Enable && c.VisitDts.Year == DateTime.Now.Year && (VehicleId > 0 ? c.VehicleId == VehicleId : true) && (dateSchedules != null ? c.VisitDts.Date == dateSchedules.Value.Date : true), (currentPage - 1) * TablesMaxRows.ScheduleIndex, TablesMaxRows.ScheduleIndex, d => d.ScheduleId, OrderBy.Ascending,new[] { "Vehicle.VehicleLicenses" });
            Schedules = Schedules.OrderBy(c => c.ScheduleId).ThenByDescending(c => c.VisitDts.Date);
            model.items = Schedules.ToList();
            var itemsCount = unitOfWork.Schedule.Count(c => c.Enable && c.VisitDts.Year == DateTime.Now.Year && (VehicleId > 0 ? c.VehicleId == VehicleId : true) && (dateSchedules != null ? c.VisitDts.Date == dateSchedules.Value.Date : true));
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.ScheduleIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.ScheduleIndex;
            return model;
        }
        public PagingViewModel<Schedule> getAllSchedulesPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.ScheduleIndex = length;
            return getAllSchedulesPaging(currentPageIndex);
        }
        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && c.LicenseNumber.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleId, label = c.LicenseNumber }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && !c.LicenseNumber.ToLower().StartsWith(prefix.ToLower()) && c.LicenseNumber.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleId, label = c.LicenseNumber }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal bool CheckIfScheduledExist(int year , int PlatNumberId)
        {
            var Schedule =  unitOfWork.Schedule.GetAllWithCriteria(c => c.Enable && c.VisitDts.Year == year &&c.VehicleId == PlatNumberId);
            if (Schedule is not null && Schedule.Count() > 0)
            {
                return true;
            }
            return false;
        }

        internal void Create(DateTime firstVisitDate, int platNumberId)
        {
            int endDate = firstVisitDate.Year;
            endDate++;
            while (firstVisitDate.Year < endDate)
            {
                unitOfWork.Schedule.Add(new Schedule()
                {
                    VehicleId = platNumberId,
                    VisitDts = firstVisitDate,
                    CreateSystemUserId = "1",
                    Enable =true,
                    CreateDts=DateTime.Now,                    
                });

                firstVisitDate = firstVisitDate.AddDays(14);
            }
            unitOfWork.Complete();
        }

        internal SelectList GetPlatNumbers()
        {
           var PlatNumbers=  unitOfWork.VehicleLicense.GetAllWithCriteria(c => c.Enable && !c.EndDate.HasValue).OrderBy(c=>c.LicenseNumber);
           SelectList items = new SelectList(PlatNumbers, "VehicleId", "LicenseNumber");
            return items;
        }
    }
}
