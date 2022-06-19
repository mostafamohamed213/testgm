using CGARMAN.Services;
using CGARMAN.ViewModel.ScheduleViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class SchedulesController : Controller
    {
        private SchedulesServices services;
        public SchedulesController(SchedulesServices _services)
        {
            services = _services;
        }
        public IActionResult Index(int CurrentPageIndex = 1, string label = null, string value=null,DateTime? dateSchedules=null)
        {
            try
            {
                //if (!string.IsNullOrWhiteSpace(label))
                //{
                //    return View(services.Search(label, 1));
                //}
                long VehicleId = 0;
                if (value is not null)
                {
                    ViewBag.label = label;
                    ViewBag.VehicleId = value;
                     VehicleId = long.Parse(value.Trim());
                }
                if (dateSchedules.HasValue)
                {
                    //string d= dateSchedules.Value.Date.ToShortDateString();
                    //d= d.Replace('/', '-');
                   // ViewBag.dateSchedules = dateSchedules.Value.Year+ "-" + dateSchedules.Value.Month+ "-" + dateSchedules.Value.Day;
                    ViewBag.dateSchedules = dateSchedules.Value.ToString("yyyy-MM-dd");

                }
               
                return View(services.getAllSchedulesPaging(CurrentPageIndex, VehicleId, dateSchedules));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }

        }
        [HttpPost]
        public IActionResult Changelength(int length, int CurrentPageIndex = 1)
        {
            try
            {
                return View("Index", services.getAllSchedulesPagingWithChangelength(CurrentPageIndex, length));
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
                var values = services.AutoComplete(prefix);
                return Json(values);
            }
            catch (Exception)
            {
                return Json(0);
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                CreateScheduleViewModel model = new CreateScheduleViewModel()
                {
                    PlatNumbers = services.GetPlatNumbers()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public IActionResult Create(CreateScheduleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (services.CheckIfScheduledExist(model.FirstVisitDate.Year,model.PlatNumberId))
                    {
                        ViewBag.ErrorMesssage = "    This vehicle already scheduled for " + model.FirstVisitDate.Year;
                        model.PlatNumbers = services.GetPlatNumbers();
                        return View(model);
                    }

                    services.Create(model.FirstVisitDate, model.PlatNumberId);
                    return RedirectToAction("Index");
                }

                model.PlatNumbers = services.GetPlatNumbers();
                
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
    }
}
