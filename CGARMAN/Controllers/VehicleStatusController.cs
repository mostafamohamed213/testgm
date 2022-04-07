using CGARMAN.Services;
using CGARMAN.ViewModel.Vehicle;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class VehicleStatusController : Controller
    {
        private VehicleStatusServices services;
        public VehicleStatusController(VehicleStatusServices _services)
        {
            services = _services;
        }
        public IActionResult Index(int CurrentPageIndex = 1, string label = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(label))
                {
                    return View(services.Search(label, 1));
                }
                return View(services.getAllVehicleStatusPaging(CurrentPageIndex));
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
                return View("Index", services.getAllVehicleStatusPagingWithChangelength(CurrentPageIndex, length));
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
        [HttpPost]
        public IActionResult Search(string label)
        {
            try
            {
                var values = services.Search(label, 1);
                return PartialView("_Search", values);
            }
            catch (Exception)
            {
                return PartialView("CustomError");
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost]
        public IActionResult Create(VehicleStatusCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.Create(model.Name);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        public IActionResult ValidateIfNameExists(string Name, string VehicleStatusId)
        {

            int id = !string.IsNullOrEmpty(VehicleStatusId) ? int.Parse(VehicleStatusId) : 0;
            bool IfExist = false;
            if (id > 0)
            {
                IfExist = services.SearchIfNameExists(Name, id);
            }
            else
            {
                IfExist = services.SearchIfNameExists(Name);
            }

            return Json(IfExist);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var status = services.getVehicleStatusById(id);
                if (status is not null)
                {
                    VehicleStatusEditViewModel model = new VehicleStatusEditViewModel()
                    {
                        VehicleStatusId = status.VehicleStatusId,
                        Name = status.Name,
                    };
                    return View(model);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public IActionResult Edit(VehicleStatusEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.VehicleStatusId > 0)
                {
                    services.Edit(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var status = services.getVehicleStatusById(id);
                if (status is not null)
                {
                    VehicleStatusEditViewModel model = new VehicleStatusEditViewModel()
                    {
                        VehicleStatusId = status.VehicleStatusId,
                        Name = status.Name,
                        CreateDts = status.CreateDts.ToString()
                    };
                    ViewBag.fromDeleteAction = true;
                    return View("View", model);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public IActionResult Delete(VehicleStatusEditViewModel model)
        {
            try
            {
                if (model.VehicleStatusId > 0)
                {
                    services.Delete(model.VehicleStatusId);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpGet]
        public IActionResult View(int id)
        {
            try
            {
                var status = services.getVehicleStatusById(id);
                if (status is not null)
                {
                    VehicleStatusEditViewModel model = new VehicleStatusEditViewModel()
                    {
                        VehicleStatusId = status.VehicleStatusId,
                        Name = status.Name,
                        CreateDts = status.CreateDts.ToString()
                    };
                    ViewBag.fromDeleteAction = false;
                    return View("View", model);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
    }
}
