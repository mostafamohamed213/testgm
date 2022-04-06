using CGARMAN.Services;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class InventoryStatusController : Controller
    {
        private InventoryServices inventoryServices;
        public InventoryStatusController(InventoryServices _inventoryServices)
        {
            inventoryServices = _inventoryServices;
        }
        public IActionResult Index(int CurrentPageIndex)
        {
            try
            {
                return PartialView("_Index", inventoryServices.getAllInventoryStatusPaging(CurrentPageIndex));
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
                return PartialView("_Index", inventoryServices.getAllInventoryItemStatusPagingWithChangelength(CurrentPageIndex, length));
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        
       public IActionResult Create(int id)
        {
            if (id <= 0)
            {
                ViewBag.fromEdit = false;
                return PartialView("_Create", new InventoryItemStatus());
            }
            try
            {
                var staus = inventoryServices.getStatusById(id);
                if (staus is not null)
                {
                    ViewBag.fromEdit = true;
                    return PartialView("_Create", staus);
                }

                return PartialView("CustomError", null);
            }
            catch (Exception )
            {
                return PartialView("CustomError", null);
            }
        }
        [HttpPost]
        public IActionResult Create(InventoryItemStatus status)
        {
            try
            {
                if (ModelState.IsValid && !string.IsNullOrEmpty(status.Name))
                {
                    if (status.InventoryItemStatusId > 0)
                    {
                        inventoryServices.UpdateStatus(status);
                    }
                    else
                    {
                        inventoryServices.AddStatus(status.Name);

                    }
                    return RedirectToAction("Index", new { CurrentPageIndex = 1 });
                }
                if (status.InventoryItemStatusId > 0)
                {
                    ViewBag.fromEdit = true;

                }
                else
                {
                    ViewBag.fromEdit = false;
                }
                return PartialView("_Create", status);
                
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
              var status =  inventoryServices.DeleteStauts(id);
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
