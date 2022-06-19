using CGARMAN.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class MaintenancesController : Controller
    {
        private MaintenancesServices services;
        public MaintenancesController(MaintenancesServices _services)
        {
            services = _services;
        }
        public IActionResult Index(int CurrentPageIndex = 1, long VehicleId = 0, string PlatNumber = null, long WorkOrderNumber = 0, DateTime? date = null, int WorkOrderStatus = 0)
        {
            try
            {

                ViewBag.VehicleId = VehicleId;
                ViewBag.PlatNumber = PlatNumber;
                ViewBag.WorkOrderNumber = WorkOrderNumber;              
                ViewBag.WorkOrderStatus = WorkOrderStatus;                
                if (date.HasValue)               
                {                   
                    ViewBag.date = date.Value.ToString("yyyy-MM-dd");
                }

                return View(services.getAllWorkOrdersPaging(CurrentPageIndex, VehicleId, WorkOrderNumber, WorkOrderStatus, date));
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
                return View("Index", services.getAllWorkOrdersPagingWithChangelength(CurrentPageIndex, length));
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
    }
}
