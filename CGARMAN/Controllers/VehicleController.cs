using CGARMAN.Services;
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
                if (!string.IsNullOrWhiteSpace(PlateNumber) || DepartmentID > 0 || OwnerID > 0 || StatusID > 0 || FamilyID > 0 || BrandID > 0)
                {
                    var values = services.getAllVehiclesPaging(CurrentPageIndex,PlateNumber, DepartmentID, OwnerID, StatusID, FamilyID, BrandID);
                    ViewBag.PlateNumber = PlateNumber;
                    return View("Index", values);
                }
                return View(services.getAllVehiclesPaging(CurrentPageIndex));
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
        public IActionResult AdvancedSearch(string PlateNumber,int DepartmentID,int OwnerID,int StatusID,int FamilyID,int BrandID)
        {
            try
            {
               // PrepareSearch();
                var values = services.AdvancedSearch(PlateNumber,DepartmentID,OwnerID,StatusID,FamilyID,BrandID);
                return View("Index", values);
            }
            catch (Exception)
            {
                return PartialView("CustomError");
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
    }
}
