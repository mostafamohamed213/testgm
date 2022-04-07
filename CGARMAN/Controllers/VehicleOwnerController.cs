using CGARMAN.Services;
using CGARMAN.ViewModel.Vehicle;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class VehicleOwnerController : Controller
    {
        private VehicleOwnerServices services;
        public VehicleOwnerController(VehicleOwnerServices _services)
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
                return View(services.getAllVehicleOwnerPaging(CurrentPageIndex));
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
                return View("Index", services.getAllVehicleOwnerPagingWithChangelength(CurrentPageIndex, length));
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
        public IActionResult Create(VehicleOwnerCreateViewModel model)
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
        public IActionResult ValidateIfNameExists(string Name, string VehicleOwnerId)
        {

            int id = !string.IsNullOrEmpty(VehicleOwnerId) ? int.Parse(VehicleOwnerId) : 0;
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
                var owner = services.getVehicleOwnerById(id);
                if (owner is not null)
                {
                    VehicleOwnerEditViewModel model = new VehicleOwnerEditViewModel()
                    {
                        VehicleOwnerId = owner.VehicleOwnerId,
                        Name = owner.Name,
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
        public IActionResult Edit(VehicleOwnerEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.VehicleOwnerId > 0)
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
                var owner = services.getVehicleOwnerById(id);
                if (owner is not null)
                {
                    VehicleOwnerEditViewModel model = new VehicleOwnerEditViewModel()
                    {
                        VehicleOwnerId = owner.VehicleOwnerId,
                        Name = owner.Name,
                        CreateDts = owner.CreateDts.ToString()
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
        public IActionResult Delete(VehicleOwnerEditViewModel model)
        {
            try
            {
                if (model.VehicleOwnerId > 0)
                {
                    services.Delete(model.VehicleOwnerId);
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
                var owner = services.getVehicleOwnerById(id);
                if (owner is not null)
                {
                    VehicleOwnerEditViewModel model = new VehicleOwnerEditViewModel()
                    {
                        VehicleOwnerId = owner.VehicleOwnerId,
                        Name = owner.Name,
                        CreateDts = owner.CreateDts.ToString()
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
