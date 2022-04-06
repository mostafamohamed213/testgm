using CGARMAN.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class InventoryClassificationsController : Controller
    {
        private InventoryServices inventoryServices;
        public InventoryClassificationsController(InventoryServices _inventoryServices)
        {
            inventoryServices = _inventoryServices;
        }
        public IActionResult Index(int CurrentPageIndex)
        {
            try
            {
                return PartialView("_Index", inventoryServices.getAllInventoryClassificationsPaging(CurrentPageIndex));
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
                return PartialView("_Index", inventoryServices.getAllInventoryClassificationPagingWithChangelength(CurrentPageIndex, length));
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
                    ViewBag.warehouses = inventoryServices.GetWarehouses();
                    return PartialView("_Create", new InventoryItemTypeClassification());
                }
                var Classification = inventoryServices.getClassificationById(id);
                var WarehouseId = Classification.WarehouseInventoryItemTypeClassifications.FirstOrDefault().WarehouseId;
                //ViewBag.warehouses = inventoryServices.GetWarehouses(inventoryServices.GetWarehouseByid(WarehouseId));
                var list = new List<Warehouse>();
                list.Add(inventoryServices.GetWarehouseByid(WarehouseId));
                ViewBag.warehouses = new SelectList(list, "WarehouseId", "Name");
                if (Classification is not null)
                {                  
                    ViewBag.fromEdit = true;
                    ViewBag.warehouseId222 = WarehouseId;
                    return PartialView("_Create", Classification);
                }

                return PartialView("CustomError", null);
            }
            catch (Exception)
            {
                return PartialView("CustomError", null);
            }
        }
        [HttpPost]
        public IActionResult Create(InventoryItemTypeClassification classification ,long warehouseId)
        {
            try
            {
                if (ModelState.IsValid && !string.IsNullOrEmpty(classification.Name) && warehouseId > 0 )
                {
                    if (classification.InventoryItemTypeClassificationId > 0)
                    {
                        var status = inventoryServices.UpdateClassification(classification, warehouseId);
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
                        if (inventoryServices.AddClassification(classification , warehouseId) > 0)
                        {
                            return Json(1);
                        }
                        else
                        {
                            return Json(-1);
                        }
                    }
                }
                if (classification.InventoryItemTypeClassificationId > 0)
                {
                    ViewBag.fromEdit = true;
                    var Classification = inventoryServices.getClassificationById(classification.InventoryItemTypeClassificationId);
                    var WarehouseId = Classification.WarehouseInventoryItemTypeClassifications.FirstOrDefault().WarehouseId;
                    var list = new List<Warehouse>();
                    list.Add(inventoryServices.GetWarehouseByid(WarehouseId));
                    ViewBag.warehouses = new SelectList(list, "WarehouseId", "Name");
                    ViewBag.warehouseId222 = WarehouseId;
                }
                else
                {
                    ViewBag.warehouses = inventoryServices.GetWarehouses();
                    ViewBag.fromEdit = false;
                }
                ViewBag.ValMassage = "Please check your input ";
                return PartialView("_Create", classification);

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
                var status = inventoryServices.DeleteClassification(id);
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
                var values = inventoryServices.AutoCompleteInventoryClassification(prefix);
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

                var values = inventoryServices.SearchInventoryClassification(elementID, label);
                return PartialView("_Search", values);
            }
            catch (Exception)
            {
                return PartialView("CustomError");
            }

        }
    }
}
