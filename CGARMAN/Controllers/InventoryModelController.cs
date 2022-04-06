using CGARMAN.Services;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class InventoryModelController : Controller
    {
        private InventoryServices inventoryServices;
        public InventoryModelController(InventoryServices _inventoryServices)
        {
            inventoryServices = _inventoryServices;
        }
        public IActionResult Index(int CurrentPageIndex)
        {
            try
            {
                ViewBag.Brands = inventoryServices.GetBrands();
                return PartialView("_Index", inventoryServices.getAllInventoryModelsPaging(CurrentPageIndex));
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
                return PartialView("_Index", inventoryServices.getAllInventoryModelPagingWithChangelength(CurrentPageIndex, length));
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            
            try
            {
                if (id <= 0)
                {
                    ViewBag.fromEdit = false;
                    ViewBag.Brands = inventoryServices.GetBrands();
                    return PartialView("_Create", new Model());

                }
                var Model = inventoryServices.getModelById(id);
                if (Model is not null)
                {
                    ViewBag.Brands = inventoryServices.GetListBrands();
                    ViewBag.fromEdit = true;
                    return PartialView("_Create", Model);
                }

                return PartialView("CustomError", null);
            }
            catch (Exception)
            {
                return PartialView("CustomError", null);
            }
        }
        [HttpPost]
        public IActionResult Create(Model model)
        {
            try
            {
                if (ModelState.IsValid && !string.IsNullOrEmpty(model.Name) && model.BrandId > 0)
                {
                    if (model.ModelId > 0)
                    {
                        var status = inventoryServices.UpdateModel(model);
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
                        if (inventoryServices.AddModel(model) > 0)
                        {
                            return Json(1);
                        }
                        else
                        {
                            return Json(-1);
                        }
                    }
                }
                if (model.ModelId > 0)
                {
                    ViewBag.fromEdit = true;
                    ViewBag.Brands = inventoryServices.GetListBrands();
                }
                else
                {
                    ViewBag.fromEdit = false;                  
                    ViewBag.Brands = inventoryServices.GetBrands();
                }
               
                ViewBag.ValMassage = "Please check your input ";
                return PartialView("_Create", model);

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
                var status = inventoryServices.DeleteModel(id);
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
                var values = inventoryServices.AutoCompleteInventoryModel(prefix);
                return Json(values);
            }
            catch (Exception)
            {
                return Json(0);
            }

        }
        [HttpPost]
        public IActionResult Search(int elementID, string label, int BrandId)
        {
            try
            {

                var values = inventoryServices.SearchInventoryModel(elementID, label, BrandId);
                return PartialView("_Search", values);
            }
            catch (Exception)
            {
                return PartialView("CustomError");
            }

        }
    }
}
