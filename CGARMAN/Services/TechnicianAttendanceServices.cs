using CGARMAN.ViewModel;
using CGARMAN.ViewModel.TechnicianViewModels;
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
    public class TechnicianAttendanceServices
    {
        IUnitOfWork unitOfWork;
        public TechnicianAttendanceServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        internal PagingViewModel<Technician> GetTechnicians(int currentPage)
        {
           
            PagingViewModel<Technician> model = new PagingViewModel<Technician>();

            List<Technician> Technicians = unitOfWork.Technician.
                                  FindAll(c => c.Enable , (currentPage - 1) * TablesMaxRows.AttendanceIndex, TablesMaxRows.AttendanceIndex, d => d.TechnicianId, OrderBy.Ascending, includes: new[] { "TechnicianCompany", "TechnicianPosition" }).ToList();
            int itemsCount = unitOfWork.Technician.Count(c => c.Enable);
            foreach (var item in Technicians)
            {
                TechnicianAttendance attendance = unitOfWork.TechnicianAttendance.GetAllWithCriteria(c => c.EventDate.Date == DateTime.Now.Date && c.TechnicianId == item.TechnicianId).FirstOrDefault();
                if (attendance is not null)
                {
                    item.TechnicianAttendances.Add(attendance);
                }
            }
            model.items = Technicians;
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.AttendanceIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.AttendanceIndex;
            return model;
        }


        internal SelectList GetAllStatus(int StatusId = 0)
        {
            var Status = unitOfWork.AttendanceStatus.GetAll();
            if (Status.Count() > 0)
            {
                if (StatusId > 0)
                {
                    return new SelectList(Status, "AttendanceStatusId", "Name", StatusId);
                }
                return new SelectList(Status, "AttendanceStatusId", "Name");
            }
            return null;
        }
        internal SelectList GetAllShifts(int ShiftId = 0)
        {
            var Shifts = unitOfWork.Shift.GetAllWithCriteria(c => c.Enable);
            if (Shifts.Count() > 0)
            {
                if (ShiftId > 0)
                {
                    return new SelectList(Shifts, "ShiftId", "Name", ShiftId);
                }
                return new SelectList(Shifts, "ShiftId", "Name");
            }
            return null;
        }

        internal void Save(int technicianId, DateTime dateEvent, int shiftId, int statusId)
        {
            var exist = unitOfWork.TechnicianAttendance.GetOne(c => c.EventDate.Date == dateEvent.Date && c.TechnicianId == technicianId);
            if (exist is not null)
            {
                TechnicianAttendanceLog log = new TechnicianAttendanceLog()
                {
                    CreateDts = DateTime.Now,
                    AttendanceStatusId = statusId,
                    ShiftId = shiftId,
                    EventDate = dateEvent,
                    TechnicianAttendanceStatusLogId = 2,
                    TechnicianId = technicianId,
                    Systemusercrate = "1",
                    Object = $"old values ( AttendanceStatusId = {exist.AttendanceStatusId} , ShiftId {exist.ShiftId} )"
                };
                exist.AttendanceStatusId = statusId;
                exist.ShiftId = shiftId;
                unitOfWork.TechnicianAttendanceLog.Add(log);
            }
            else
            {
                DateTime dateTimenow = DateTime.Now;
                TechnicianAttendance technicianAttendance = new TechnicianAttendance()
                {
                    AttendanceStatusId = statusId,
                    CreateDts = dateTimenow,
                    EventDate = dateEvent,
                    ShiftId = shiftId,
                    Systemusercrate = "1",
                    TechnicianId = technicianId,
                };
                unitOfWork.TechnicianAttendance.Add(technicianAttendance);
                TechnicianAttendanceLog log = new TechnicianAttendanceLog()
                {
                    CreateDts = dateTimenow,
                    AttendanceStatusId = statusId,
                    ShiftId = shiftId,
                    EventDate = dateEvent,
                    TechnicianAttendanceStatusLogId = 1,
                    TechnicianId = technicianId,
                    Systemusercrate = "1",
                };
                unitOfWork.TechnicianAttendanceLog.Add(log);
            }

            unitOfWork.Complete();
        }
        internal void Delete(int technicianId, DateTime dateEvent)
        {
            var exist = unitOfWork.TechnicianAttendance.GetOne(c => c.EventDate.Date == dateEvent.Date && c.TechnicianId == technicianId);
            if (exist is not null)
            {
                TechnicianAttendanceLog log = new TechnicianAttendanceLog()
                {
                    CreateDts = DateTime.Now,
                    AttendanceStatusId = exist.AttendanceStatusId,
                    ShiftId = exist.ShiftId,
                    EventDate = dateEvent,
                    TechnicianAttendanceStatusLogId = 3,
                    TechnicianId = technicianId,
                    Systemusercrate = "1",
                };
                unitOfWork.TechnicianAttendanceLog.Add(log);
                unitOfWork.TechnicianAttendance.Delete(exist);
                unitOfWork.Complete();
            }
        }
        internal PagingViewModel<Technician> Search(int CurrentPageIndex, string Name = null, int PositionsId = -1, int CompanyId = -1)
        {
            var Technicians = unitOfWork.Technician.
                FindAll(c => c.Enable && (!string.IsNullOrWhiteSpace(Name) ? c.Name.Trim().ToLower().Contains(Name.Trim().ToLower()) : false || c.TechnicianPositionId == PositionsId || c.TechnicianCompanyId == CompanyId)
                , (CurrentPageIndex - 1) * 1000, 1000, d => d.TechnicianId, OrderBy.Ascending, includes: new[] { "TechnicianCompany", "TechnicianPosition" }).ToList();
            var itemsCount = unitOfWork.Technician.Count(c => c.Enable && (!string.IsNullOrWhiteSpace(Name) ? c.Name.Trim().ToLower().Contains(Name.Trim().ToLower()) : false || c.TechnicianPositionId == PositionsId || c.TechnicianCompanyId == CompanyId));

            var Companies = unitOfWork.TechnicianCompany.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            PagingViewModel<Technician> model = new PagingViewModel<Technician>();
            model.items = Technicians.ToList();
            foreach (var item in Technicians)
            {
                TechnicianAttendance attendance = unitOfWork.TechnicianAttendance.GetAllWithCriteria(c => c.EventDate.Date == DateTime.Now.Date && c.TechnicianId == item.TechnicianId).FirstOrDefault();
                if (attendance is not null)
                {
                    item.TechnicianAttendances.Add(attendance);
                }
            }

            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = CurrentPageIndex;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        public PagingViewModel<Technician> getAllTechniciansPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.AttendanceIndex = length;
            return GetTechnicians(currentPageIndex);
        }

        internal DaysAttendancePagingViewModel GetTechniciansDays(int currentPage)
        {
            DaysAttendancePagingViewModel model = new DaysAttendancePagingViewModel();

            List<Technician> Technicians = unitOfWork.Technician.
                                  FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.AttendanceDaysIndex, TablesMaxRows.AttendanceDaysIndex, d => d.TechnicianId, OrderBy.Ascending, includes: new[] { "TechnicianCompany", "TechnicianPosition" }).ToList();
            int itemsCount = unitOfWork.Technician.Count(c => c.Enable);
            //foreach (var item in Technicians)
            //{
            //    TechnicianAttendance attendance = unitOfWork.TechnicianAttendance.GetAllWithCriteria(c => c.EventDate.Date == DateTime.Now.Date && c.TechnicianId == item.TechnicianId).FirstOrDefault();
            //    if (attendance is not null)
            //    {
            //        item.TechnicianAttendances.Add(attendance);
            //    }
            //}
            model.items = Technicians;
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.AttendanceDaysIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.AttendanceDaysIndex;
            return model;
        }
        public DaysAttendancePagingViewModel getAllTechniciansPagingWithChangelengthDays(int currentPageIndex, int length)
        {
            TablesMaxRows.AttendanceDaysIndex = length;
            return GetTechniciansDays(currentPageIndex);
        }
        internal DaysAttendancePagingViewModel SearchDays(int CurrentPageIndex, string Name = null, int PositionsId = -1, int CompanyId = -1)
        {
            var Technicians = unitOfWork.Technician.
                FindAll(c => c.Enable && (!string.IsNullOrWhiteSpace(Name) ? c.Name.Trim().ToLower().Contains(Name.Trim().ToLower()) : false || c.TechnicianPositionId == PositionsId || c.TechnicianCompanyId == CompanyId)
                , (CurrentPageIndex - 1) * 1000, 1000, d => d.TechnicianId, OrderBy.Ascending, includes: new[] { "TechnicianCompany", "TechnicianPosition" }).ToList();
            var itemsCount = unitOfWork.Technician.Count(c => c.Enable && (!string.IsNullOrWhiteSpace(Name) ? c.Name.Trim().ToLower().Contains(Name.Trim().ToLower()) : false || c.TechnicianPositionId == PositionsId || c.TechnicianCompanyId == CompanyId));

            //var Companies = unitOfWork.TechnicianCompany.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            DaysAttendancePagingViewModel model = new DaysAttendancePagingViewModel();
            model.items = Technicians.ToList();
            //foreach (var item in Technicians)
            //{
            //    TechnicianAttendance attendance = unitOfWork.TechnicianAttendance.GetAllWithCriteria(c => c.EventDate.Date == DateTime.Now.Date && c.TechnicianId == item.TechnicianId).FirstOrDefault();
            //    if (attendance is not null)
            //    {
            //        item.TechnicianAttendances.Add(attendance);
            //    }
            //}

            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = CurrentPageIndex;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }
        internal void SaveDays(int technicianId, int shiftId, int statusId, DateTime from, DateTime? to)
        {
            if (to.HasValue)
            {
                for (var day = from.Date; day <= to; day = day.AddDays(1))
                {
                    Save(technicianId, day, shiftId, statusId);
                }
            }
            else
            {
                Save(technicianId, from, shiftId, statusId);
            }
        }
        internal void DeleteDays(int technicianId, DateTime from, DateTime? to)
        {
            if (to.HasValue)
            {
                for (var day = from.Date; day <= to; day = day.AddDays(1))
                {
                    Delete(technicianId, day);
                }
            }
            else
            {
                Delete(technicianId, from);
            }
        }
    }
}
