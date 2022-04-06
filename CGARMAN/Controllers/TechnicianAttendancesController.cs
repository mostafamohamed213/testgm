using CGARMAN.Services;
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
        public IActionResult Daily(int CurrentPageIndex = 1,string Name=null,int PositionsId= -1, int CompanyId= -1)
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
    }
}
