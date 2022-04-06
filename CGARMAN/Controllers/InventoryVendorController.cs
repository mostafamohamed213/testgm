using CGARMAN.Services;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class InventoryVendorController : Controller
    {
        private InventoryServices inventoryServices;
        public InventoryVendorController(InventoryServices _inventoryServices)
        {
            inventoryServices = _inventoryServices;
        }
        public IActionResult Index(int CurrentPageIndex)
        {
            try
            {
                return PartialView("_Index", inventoryServices.getAllVendorPaging(CurrentPageIndex));
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        [HttpPost]
        public IActionResult Changelength(int CurrentPageIndex, int length)
        {
            try
            {
                return PartialView("_Index", inventoryServices.getAllVendorPagingWithChangelength(CurrentPageIndex, length));
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        [HttpPost]
        public IActionResult Create(string vendorName)
        {
            try
            {
                if (!string.IsNullOrEmpty(vendorName.Trim()))
                {
                    var status = inventoryServices.addVendor(vendorName.Trim());
                    if (status == 1)
                    {
                        var vendors = inventoryServices.GetItemtypeVendors();
                        return Json(new { status = 1, @object = vendors });
                    }
                    if (status == -1)
                    {
                        return Json(new { status = -1, @object = "" });
                    }
                   
                }
                return Json(new { status = 0, @object = "" });
            }
            catch (Exception)
            {

                return Json(new { status = 0, @object = "" });
            }
           
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            if (id <= 0)
            {
                ViewBag.fromEdit = false;
                return PartialView("_Save", new Vendor());
            }
            try
            {
                var vendor = inventoryServices.getVendorById(id);
                if (vendor is not null)
                {
                    ViewBag.fromEdit = true;
                    return PartialView("_Save", vendor);
                }

                return PartialView("CustomError", null);
            }
            catch (Exception)
            {
                return PartialView("CustomError", null);
            }
        }
        [HttpPost]
        public IActionResult Save(Vendor vendor)
        {
            try
            {
                if (ModelState.IsValid && !string.IsNullOrEmpty(vendor.Name) )
                {
                    if (vendor.VendorId > 0)
                    {
                        var status = inventoryServices.UpdateVendor(vendor);
                        if (status == 1)
                        {
                            return Json(1);
                        }
                        else if (status == -1)
                        {
                            return Json(-1);
                        }
                        else
                        {
                            return Json(0);
                        }
                    }
                    else
                    {
                        if (inventoryServices.AddVendor(vendor) > 0)
                        {
                            return Json(1);
                        }
                        else
                        {
                            return Json(-1);
                        }
                    }
                }
                if (vendor.VendorId > 0)
                {
                    ViewBag.fromEdit = true;

                }
                else
                {
                    ViewBag.fromEdit = false;
                }
                ViewBag.ValMassage = "Please check your input ";
                return PartialView("_Save", vendor);

            }
            catch (Exception)
            {

                return PartialView("CustomError", null);
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var status = inventoryServices.DeleteVendor(id);
                if (status == 1)
                {
                    return Json(1);
                }
                else if (status == 0)
                {
                    return Json(0);
                }
                else if (status == -1)
                {
                    return Json(-1);
                }

                return Json(-1);
            }
            catch (Exception)
            {

                return PartialView("CustomError", null);
            }

        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            try
            {
                var values = inventoryServices.AutoCompleteInventoryVendor(prefix);
                return Json(values);
            }
            catch (Exception)
            {
                return Json(0);
            }

        }
        [HttpPost]
        public IActionResult Search(int elementID, string label)
        {
            try
            {

                var values = inventoryServices.SearchInventoryVendor(elementID, label);
                return PartialView("_Search", values);
            }
            catch (Exception)
            {
                return PartialView("CustomError");
            }

        }

    }
}
