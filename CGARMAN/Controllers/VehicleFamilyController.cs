using CGARMAN.Services;
using CGARMAN.ViewModel.Vehicle;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class VehicleFamilyController : Controller
    {
        private VehicleFamilyServices services;
        public VehicleFamilyController(VehicleFamilyServices _services)
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
                return View(services.getAllVehicleFamilyPaging(CurrentPageIndex));
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
                return View("Index", services.getAllVehicleFamilyPagingWithChangelength(CurrentPageIndex, length));
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
        public IActionResult Create(VehicleFamilyCreateViewModel model)
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
        public IActionResult ValidateIfNameExists(string Name, string VehicleFamilyId)
        {

            int id = !string.IsNullOrEmpty(VehicleFamilyId) ? int.Parse(VehicleFamilyId) : 0;
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
                var Family = services.getVehicleFamilyById(id);
                if (Family is not null)
                {
                    VehicleFamilyEditViewModel model = new VehicleFamilyEditViewModel()
                    {
                        VehicleFamilyId = Family.VehicleFamilyId,
                        Name = Family.Name,
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
        public IActionResult Edit(VehicleFamilyEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.VehicleFamilyId > 0)
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
                var Family = services.getVehicleFamilyById(id);
                if (Family is not null)
                {
                    VehicleFamilyEditViewModel model = new VehicleFamilyEditViewModel()
                    {
                        VehicleFamilyId = Family.VehicleFamilyId,
                        Name = Family.Name,
                        CreateDts = Family.CreateDts.ToString()
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
        public IActionResult Delete(VehicleFamilyEditViewModel model)
        {
            try
            {
                if (model.VehicleFamilyId > 0)
                {
                    services.Delete(model.VehicleFamilyId);
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
                var Family = services.getVehicleFamilyById(id);
                if (Family is not null)
                {
                    VehicleFamilyEditViewModel model = new VehicleFamilyEditViewModel()
                    {
                        VehicleFamilyId = Family.VehicleFamilyId,
                        Name = Family.Name,
                        CreateDts = Family.CreateDts.ToString()
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
