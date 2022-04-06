using CGARMAN.Services;
using CGARMAN.ViewModel.TechnicianViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CGARMAN.Controllers
{
    public class TechnicianAttendancesController : Controller
    {
        private TechnicianAttendanceServices servicesAttendances;
        private TechniciansServices servicesTechnicians;
        public TechnicianAttendancesController(TechnicianAttendanceServices _servicesAttendances, TechniciansServices _servicesTechnicians)
        {
            servicesAttendances = _servicesAttendances;
            servicesTechnicians = _servicesTechnicians;
        }
        [HttpGet]
        public IActionResult Daily(int CurrentPageIndex = 1, string Name = null, int PositionsId = -1, int CompanyId = -1)
        {
            try
            {
                ViewBag.Companies = servicesTechnicians.GetAllCompanies(CompanyId);
                ViewBag.Positions = servicesTechnicians.GetAllTechnicianPositions(PositionsId);

                ViewBag.Name = Name;
                ViewBag.Status = servicesAttendances.GetAllStatus();
                ViewBag.Shifts = servicesAttendances.GetAllShifts();
                if (!string.IsNullOrWhiteSpace(Name) || PositionsId > 0 || CompanyId > 0)
                {
                    return View(servicesAttendances.Search(1, Name, PositionsId, CompanyId));
                }
                return View(servicesAttendances.GetTechnicians(CurrentPageIndex));
            }
            catch (Exception ex)
            {

                return View("Error", ex);
            }

        }
        [HttpPost]
        public IActionResult ChangelengthDaily(int length, int CurrentPageIndex = 1)
        {
            try
            {
                ViewBag.Companies = servicesTechnicians.GetAllCompanies();
                ViewBag.Positions = servicesTechnicians.GetAllTechnicianPositions();
                ViewBag.Status = servicesAttendances.GetAllStatus();
                ViewBag.Shifts = servicesAttendances.GetAllShifts();
                return View("Daily", servicesAttendances.getAllTechniciansPagingWithChangelength(CurrentPageIndex, length));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            try
            {
                var values = servicesTechnicians.AutoCompleteTechnicianName(prefix);
                return Json(values);
            }
            catch (Exception)
            {
                return Json(0);
            }

        }
        [HttpPost]
        public JsonResult Save(int technicianId, DateTime? dateEvent, int shiftId, int statusId)
        {
            try
            {
                if (technicianId > 0 && dateEvent.HasValue && shiftId > 0 && statusId > 0)
                {
                    servicesAttendances.Save(technicianId, dateEvent.Value, shiftId, statusId);
                    return Json(1);
                }
                return Json(0);
            }
            catch (Exception)
            {

                return Json(-1);
            }
        }
        [HttpPost]
        public JsonResult Delete(int technicianId, DateTime? dateEvent)
        {
            try
            {
                if (technicianId > 0 && dateEvent.HasValue)
                {
                    servicesAttendances.Delete(technicianId, dateEvent.Value);
                    return Json(1);
                }
                return Json(0);
            }
            catch (Exception)
            {

                return Json(-1);
            }
        }
        [HttpGet]
        public IActionResult Days(int CurrentPageIndex = 1, string Name = null, int PositionsId = -1, int CompanyId = -1)
        {
            try
            {
                DaysAttendancePagingViewModel model = null;

                if (!string.IsNullOrWhiteSpace(Name) || PositionsId > 0 || CompanyId > 0)
                {
                    model = servicesAttendances.SearchDays(1, Name, PositionsId, CompanyId);
                }
                else
                {
                    model = servicesAttendances.GetTechniciansDays(CurrentPageIndex);
                }
                model.Companies = servicesTechnicians.GetAllCompanies(CompanyId);
                model.Positions = servicesTechnicians.GetAllTechnicianPositions(PositionsId);
                model.Shifts = servicesAttendances.GetAllShifts();
                model.Status = servicesAttendances.GetAllStatus();
                model.Name = Name;
                return View(model);
            }
            catch (Exception ex)
            {

                return View("Error", ex);
            }

        }
        [HttpPost]
        public IActionResult ChangelengthDays(int length, int CurrentPageIndex = 1)
        {
            try
            {
                DaysAttendancePagingViewModel model = servicesAttendances.getAllTechniciansPagingWithChangelengthDays(CurrentPageIndex, length);
                model.Companies = servicesTechnicians.GetAllCompanies();
                model.Positions = servicesTechnicians.GetAllTechnicianPositions();
                model.Status = servicesAttendances.GetAllStatus();
                model.Shifts = servicesAttendances.GetAllShifts();
                return View("Days", model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public JsonResult SaveDays(int technicianId, int shiftId, int statusId, DateTime? from, DateTime? to)
        {
            try
            {
                if (technicianId > 0 && shiftId > 0 && statusId > 0 && from.HasValue)
                {
                    servicesAttendances.SaveDays(technicianId, shiftId, statusId, from.Value, to);
                    return Json(1);
                }
                return Json(0);
            }
            catch (Exception)
            {

                return Json(-1);
            }
        }
        [HttpPost]
        public JsonResult DeleteDays(int technicianId, DateTime? from, DateTime? to)
        {
            try
            {
                if (technicianId > 0 && from.HasValue)
                {
                    servicesAttendances.DeleteDays(technicianId, from.Value, to);
                    return Json(1);
                }
                return Json(0);
            }
            catch (Exception)
            {

                return Json(-1);
            }
        }
    }
}
