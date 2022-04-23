using CGARMAN.Services;
using CGARMAN.ViewModel.Vehicle;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class VehicleController : Controller
    {
        private VehicleServices services;
        public VehicleController(VehicleServices _services)
        {
            services = _services;
        }
        public IActionResult Index(int CurrentPageIndex = 1, string label = null , string PlateNumber =null, int DepartmentID=0, int OwnerID=0, int StatusID=0, int FamilyID=0, int BrandID=0)
        {
            try
            {
                PrepareSearch(DepartmentID, OwnerID, StatusID, FamilyID, BrandID);
                if (!string.IsNullOrWhiteSpace(label))
                {
                    ViewBag.label = label;
                    return View(services.Search(label, 1));
                }
                 var values = services.getAllVehiclesPaging(CurrentPageIndex,PlateNumber, DepartmentID, OwnerID, StatusID, FamilyID, BrandID);
                 ViewBag.PlateNumber = PlateNumber;
                 return View("Index", values);               
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
                PrepareSearch();
                return View("Index", services.getAllVehiclesPagingWithChangelength(CurrentPageIndex, length));
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
        public JsonResult getBrandByfamilyId(int FamilyId ,int BrandId =0)
        {          
            try
            {
                var Brands = services.GetBrandsByfamilyId(FamilyId, BrandId);               
                return Json( new { status = 1, @object = Brands });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, @object = ex });
            }
        }
        [HttpPost]
        public JsonResult getPlateNumberByBrandId(int BrandId, int vehicleId = 0)
        {
            try
            {
                var PlateNumbers = services.getPlateNumberByBrandId(BrandId, vehicleId);
                return Json(new { status = 1, @object = PlateNumbers });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, @object = ex });
            }
        }
        [NonAction]
        private void PrepareSearch(int DepartmentID = 0, int OwnerID = 0, int StatusID = 0, int FamilyID = 0, int BrandID = 0)
        {
            ViewBag.Departments = services.GetDepartments(DepartmentID);           
            ViewBag.Status = services.GetStatus(StatusID);
            ViewBag.Owners = services.GetOwners(OwnerID);
            ViewBag.Families = services.GetFamilies(FamilyID);
            if (BrandID > 0 && FamilyID > 0)
            {
                ViewBag.Brands = services.GetBrandsByfamilyId(FamilyID, BrandID);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            try
            {              
                return View(services.PrepareCreate(new VehicleCreateViewModel()));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }      
       
        [HttpPost]
        public IActionResult Create(VehicleCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fName = services.getFamilyNameById(model.VehicleFamilyId);
                    long vehicleId = 0;
                    if (fName.ToLower() == "silo" || fName.ToLower() == "trailer")
                    {
                        vehicleId = services.Create(model);
                    }
                    else
                    {
                       
                        if(string.IsNullOrEmpty(model.Capacity))
                        {
                            ModelState.AddModelError("Capacity", "This field is required");
                        }
                        if (string.IsNullOrEmpty(model.ChassisSerial))
                        {
                            ModelState.AddModelError("ChassisSerial", "This field is required");
                        }
                        if (string.IsNullOrEmpty(model.ChassisType))
                        {
                            ModelState.AddModelError("ChassisType", "This field is required");
                        }
                        if (string.IsNullOrEmpty(model.EngineSerial))
                        {
                            ModelState.AddModelError("EngineSerial", "This field is required");
                        }
                        if (string.IsNullOrEmpty(model.EngineType))
                        {
                            ModelState.AddModelError("EngineType", "This field is required");
                        }
                        if (string.IsNullOrEmpty(model.Capacity) || string.IsNullOrEmpty(model.ChassisSerial) || string.IsNullOrEmpty(model.ChassisType)|| string.IsNullOrEmpty(model.EngineSerial) || string.IsNullOrEmpty(model.EngineType))
                        {
                            return View(services.PrepareCreate(model));
                        }
                        else
                        {
                            vehicleId = services.Create(model);
                        }
                    }
                    if (vehicleId > 0)
                    {
                        return RedirectToAction("View", new { id = vehicleId });
                    }
                    else
                    {
                        return View("Error");
                    }
                   
                }
                return View(services.PrepareCreate(model));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var model = services.getVehicleViewModelById(id);
                if (model.VehicleId > 0)
                {                  
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
        public IActionResult Edit(VehicleCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.VehicleId > 0)
                {

                    string fName = services.getFamilyNameById(model.VehicleFamilyId);
                    long vehicleId = 0;
                    if (fName.ToLower() == "silo" || fName.ToLower() == "trailer")
                    {
                         vehicleId = services.Edit(model);
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(model.Capacity))
                        {
                            ModelState.AddModelError("Capacity", "This field is required");
                        }
                        if (string.IsNullOrEmpty(model.ChassisSerial))
                        {
                            ModelState.AddModelError("ChassisSerial", "This field is required");
                        }
                        if (string.IsNullOrEmpty(model.ChassisType))
                        {
                            ModelState.AddModelError("ChassisType", "This field is required");
                        }
                        if (string.IsNullOrEmpty(model.EngineSerial))
                        {
                            ModelState.AddModelError("EngineSerial", "This field is required");
                        }
                        if (string.IsNullOrEmpty(model.EngineType))
                        {
                            ModelState.AddModelError("EngineType", "This field is required");
                        }
                        if (string.IsNullOrEmpty(model.Capacity) || string.IsNullOrEmpty(model.ChassisSerial) || string.IsNullOrEmpty(model.ChassisType) || string.IsNullOrEmpty(model.EngineSerial) || string.IsNullOrEmpty(model.EngineType))
                        {
                            return View(services.PrepareCreate(model));
                        }
                        else
                        {
                            vehicleId = services.Edit(model);
                        }
                    }
                    if (vehicleId > 0)
                    {
                        return RedirectToAction("View", new { id = vehicleId });
                    }
                    else
                    {
                        return View("Error");
                    }
                   
                }
                services.PrepareCreate(model);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        public IActionResult ValidateIfPlateNumberExists(string LicenseNumber, string VehicleId)
        {

            int id = !string.IsNullOrEmpty(VehicleId) ? int.Parse(VehicleId) : 0;
            bool IfExist = false;
            if (id > 0)
            {
                IfExist = services.SearchIfLicenseNumberExists(LicenseNumber, id);
            }
            else
            {
                IfExist = services.SearchIfLicenseNumberExists(LicenseNumber);
            }

            return Json(IfExist);
        }
        [HttpGet]
        public IActionResult View(long id)
        {
            try
            {
                var model = services.ViewVehicle(id);
                if (model is not null)
                {
                    ViewBag.fromDeleteAction = false;
                    return View(model);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpGet]
        public IActionResult TrailerHistory(long VehicleId)
        {
            try
            {
                var model = services.GetTrailerHistory(VehicleId);
                return PartialView("_TrailerHistory",model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpGet]
        public IActionResult LinkTrailer(long VehicleId)
        {
            try
            {
                var vehicle = services.GetVehicleById(VehicleId);
                if (vehicle is not null)
                {
                    LinkTrailerViewModel model = new LinkTrailerViewModel
                    {
                        VehicleID = VehicleId,
                        Vehicle = vehicle,
                        Families = services.GetFamiliesForlinkTrailer()
                    };
                    return View("LinkTrailer", model);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        public JsonResult ValidatePlateNumber(string PlateNumberTrailer)
        {        
            if ( !string.IsNullOrWhiteSpace(PlateNumberTrailer))
            {
                return Json(services.ValidateplateNumberExistsAndFree(PlateNumberTrailer));
            }           
            return Json(false);

        }
        public JsonResult ValidatePlateNumber2(long TrailerID)
        {
            if (TrailerID > 0)
            {
                return Json(services.ValidateplateNumberExistsAndFree2(TrailerID));
            }
            return Json(false);

        }
        [HttpPost]
        public JsonResult AutoCompleteLinkTrailer(string prefix)
        {
            try
            {
                var values = services.AutoCompleteLinkTrailer(prefix);
                return Json(values);
            }
            catch (Exception)
            {
                return Json(0);
            }

        }
        [HttpPost]
        public IActionResult LinkTrailer(long VehicleID, long TrailerID)
        {
            if (VehicleID > 0 && TrailerID > 0)
            {
                long vehicleId = services.LinkTrailer(VehicleID, TrailerID);
                if (vehicleId > 0)
                {
                    return RedirectToAction("View", new { id = vehicleId });
                }
            }         
            return View("Error");
        }
        [HttpPost]
        public IActionResult UnlinkTrailer(long VehicleId,long TrailerId)
        {
            try
            {
                int status = services.UnlinkTrailer(VehicleId, TrailerId);
                if (status == 1)
                {
                    return Json(1);
                } 
                return Json(-1);
            }
            catch (Exception)
            {
                return Json(-1);
            }

        }
        [HttpGet]
        public IActionResult Delete(long id)
        {
            try
            {
                var model = services.ViewVehicle(id);
                if (model is not null)
                {                   
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
        public IActionResult Delete(VehicleViewViewModel model)
        {
            try
            {
                if (model.VehicleId > 0)
                {                  
                    if (services.Delete(model.VehicleId) > 0)
                    {
                        return RedirectToAction("Index");
                    }                  
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
