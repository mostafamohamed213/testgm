using CGARMAN.Services;
using CGARMAN.ViewModel.Vehicle;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class VehicleBrandController : Controller
    {
        private VehicleBrandServices services;
        public VehicleBrandController(VehicleBrandServices _services)
        {
            services = _services;
        }
        public IActionResult Index(int CurrentPageIndex = 1, string label = null, int FamilyId = -1)
        {
            try
            {
                ViewBag.Families = services.GetAllFamilies(FamilyId);
                if (!string.IsNullOrWhiteSpace(label)|| FamilyId > 0)
                {
                    return View(services.Search(label,1, FamilyId));
                }
                return View(services.getAllVehicleBrandsPaging(CurrentPageIndex));
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
                ViewBag.Families = services.GetAllFamilies();
                return View("Index", services.getAllVehicleBrandsPagingWithChangelength(CurrentPageIndex, length));
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
        //[HttpPost]
        //public IActionResult Search(string label)
        //{
        //    try
        //    {
        //        var values = services.Search(label, 1);
        //        return PartialView("_Search", values);
        //    }
        //    catch (Exception)
        //    {
        //        return PartialView("CustomError");
        //    }

        //}

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                VehicleBrandCreateViewModel model = new VehicleBrandCreateViewModel()
                {
                    Families = services.GetAllFamilies()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost]
        public IActionResult Create(VehicleBrandCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.Create(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        public IActionResult ValidateIfNameExists(string Name,int VehicleFamilyId, string VehicleBrandId)
        {

            int id = !string.IsNullOrEmpty(VehicleBrandId) ? int.Parse(VehicleBrandId) : 0;
            bool IfExist = false;
            if (id > 0)
            {
                IfExist = services.SearchIfNameExists(Name, VehicleFamilyId, id);
            }
            else
            {
                IfExist = services.SearchIfNameExists(Name, VehicleFamilyId);
            }

            return Json(IfExist);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var brand = services.getVehicleBrandById(id);
                if (brand is not null)
                {
                    VehicleBrandEditViewModel model = new VehicleBrandEditViewModel()
                    {
                        VehicleBrandId = brand.VehicleBrandId,
                        Name = brand.Name,
                        VehicleFamilyId = brand.VehicleFamilyId,
                        Families =services.GetAllFamilies()
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
        public IActionResult Edit(VehicleBrandEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.VehicleBrandId > 0)
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
                var brand = services.getVehicleBrandById(id);
                if (brand is not null)
                {
                    VehicleBrandEditViewModel model = new VehicleBrandEditViewModel()
                    {
                        VehicleBrandId = brand.VehicleBrandId,
                        Name = brand.Name,
                        CreateDts = brand.CreateDts.ToString(),
                        FamilyName = brand.VehicleFamily.Name
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
        public IActionResult Delete(VehicleBrandEditViewModel model)
        {
            try
            {
                if (model.VehicleBrandId > 0)
                {
                    services.Delete(model.VehicleBrandId);
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
                var brand = services.getVehicleBrandById(id);
                if (brand is not null)
                {
                    VehicleBrandEditViewModel model = new VehicleBrandEditViewModel()
                    {
                        VehicleBrandId = brand.VehicleBrandId,
                        Name = brand.Name,
                        CreateDts = brand.CreateDts.ToString(),
                        FamilyName = brand.VehicleFamily.Name
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
