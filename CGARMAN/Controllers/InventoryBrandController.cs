using CGARMAN.Services;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class InventoryBrandController : Controller
    {
        private InventoryServices inventoryServices;
        public InventoryBrandController(InventoryServices _inventoryServices)
        {
            inventoryServices = _inventoryServices;
        }
        public IActionResult Index(int CurrentPageIndex)
        {
            try
            {
                return PartialView("_Index", inventoryServices.getAllInventoryBrandsPaging(CurrentPageIndex));
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
                return PartialView("_Index", inventoryServices.getAllInventoryBrandPagingWithChangelength(CurrentPageIndex, length));
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            if (id <= 0)
            {
                ViewBag.fromEdit = false;
                return PartialView("_Create", new Brand());
            }
            try
            {
                var Brand = inventoryServices.getBrandById(id);
                if (Brand is not null)
                {
                    ViewBag.fromEdit = true;
                    return PartialView("_Create", Brand);
                }

                return PartialView("CustomError", null);
            }
            catch (Exception)
            {
                return PartialView("CustomError", null);
            }
        }
        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            try
            {
                if (ModelState.IsValid && !string.IsNullOrEmpty(brand.Name))
                {
                    if (brand.BrandId > 0)
                    {
                        var status = inventoryServices.UpdateBrand(brand);
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
                        if (inventoryServices.AddBrand(brand) > 0)
                        {
                            return Json(1);
                        }
                        else
                        {
                            return Json(-1);
                        }
                    }
                }
                if (brand.BrandId > 0)
                {
                    ViewBag.fromEdit = true;

                }
                else
                {
                    ViewBag.fromEdit = false;
                }
                ViewBag.ValMassage = "Please check your input ";
                return PartialView("_Create", brand);

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
                var status = inventoryServices.DeleteBrand(id);
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
                var values = inventoryServices.AutoCompleteInventoryBrand(prefix);
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

                var values = inventoryServices.SearchInventoryBrand(elementID, label);
                return PartialView("_Search", values);
            }
            catch (Exception)
            {
                return PartialView("CustomError");
            }

        }
    }
}
