using CGARMAN.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class InventoryWarehouseController : Controller
    {
        private InventoryServices inventoryServices;
        public InventoryWarehouseController(InventoryServices _inventoryServices)
        {
            inventoryServices = _inventoryServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetSubWarehouseStructure(long warehouseId)
        {
            try
            {
                var structure = inventoryServices.GetSubWarehouseStructure(warehouseId);
                if (structure == null)
                {
                    return Content("null");
                }               
                return PartialView("_GetSubWarehouseStructure", structure);
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }          
        }
        public IActionResult GetLaneWarehouseStructure(long subWarehouseId)
        {
            try
            {
                var structure = inventoryServices.GetLaneWarehouseStructure(subWarehouseId);
                if (structure == null)
                {
                    return Content("null");
                }
                return PartialView("_GetLaneWarehouseStructure", structure);
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        public IActionResult GetShelfWarehouseStructure(long laneWarehouseId)
        {
            try
            {
                var structure = inventoryServices.GetShelfWarehouseStructure(laneWarehouseId);
                
                if (structure == null)
                {
                    return Content("null");
                }
                return PartialView("_GetShelfWarehouseStructure", structure);
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        public IActionResult GetWarehouseTypes(long warehouseId)
        {
            try
            {               
                var types = inventoryServices.GetWarehouseTypes(warehouseId);
                //if (types == null)
                //{
                //    return Content("null");
                //}
                ViewBag.warehouseId = warehouseId;
                ViewBag.WarehouseTypes = types;
                return PartialView("_GetWarehouseTypes");
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        
        public IActionResult getValuesViscosity()
        {
            try
            {
                var Values = inventoryServices.getValuesViscosity();
                return Json(Values);
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }

        public IActionResult getValuesTireSizeAndTirePattern()
        {
            try
            {
                var tireSizes = inventoryServices.getValuesTireSize();
                var tirePatterns = inventoryServices.getValuesTirePattern();
                return Json(new { tirePatterns, tireSizes });
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
    }
}
