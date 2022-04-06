using CGARMAN.Services;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class InventoryUnitController : Controller
    {
        private InventoryServices inventoryServices;
        public InventoryUnitController(InventoryServices _inventoryServices)
        {
            inventoryServices = _inventoryServices;
        }
        public IActionResult Index(int CurrentPageIndex)
        {
            try
            {
                return PartialView("_Index", inventoryServices.getAllInventoryUnitsPaging(CurrentPageIndex));
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
                return PartialView("_Index", inventoryServices.getAllInventoryUnitsPagingWithChangelength(CurrentPageIndex, length));
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
                return PartialView("_Create", new InventoryItemTypeUnit());
            }
            try
            {
                var Unit = inventoryServices.getUnitById(id);
                if (Unit is not null)
                {
                    ViewBag.fromEdit = true;
                    return PartialView("_Create", Unit);
                }

                return PartialView("CustomError", null);
            }
            catch (Exception)
            {
                return PartialView("CustomError", null);
            }
        }
        [HttpPost]
        public IActionResult Create(InventoryItemTypeUnit unit)
        {
            try
            {
                if (ModelState.IsValid && !string.IsNullOrEmpty(unit.Name) &&  !string.IsNullOrEmpty(unit.Description))
                {
                    if (unit.InventoryItemTypeUnitId > 0)
                    {
                        var status = inventoryServices.UpdateUnit(unit);
                        if (status == 1)
                        {
                            return Json(1);
                        }
                        else if(status == -1)
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
                        if (inventoryServices.AddUnit(unit) > 0)
                        {
                            return Json(1);
                        }
                        else
                        {
                            return Json(-1);
                        }
                    }                    
                }
                if (unit.InventoryItemTypeUnitId > 0)
                {
                    ViewBag.fromEdit = true;

                }
                else
                {
                    ViewBag.fromEdit = false;
                }
                ViewBag.ValMassage = "Please check your input ";
                return PartialView("_Create", unit);

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
                var status = inventoryServices.DeleteUnit(id);
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
    }
}
