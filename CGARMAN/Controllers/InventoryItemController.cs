using CGARMAN.Services;
using CGARMAN.ViewModel.InventoryViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class InventoryItemController : Controller
    {
        private InventoryServices inventoryServices;
        public InventoryItemController(InventoryServices _inventoryServices)
        {
            inventoryServices = _inventoryServices;
        }
        public IActionResult Index()
        {          
            try
            {               
                return View(inventoryServices.getAllInventoryItem());
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        long PrepareCreatePageInventoryItem(int itemTypeId)
        {
            try
            {
                var ItemType = inventoryServices.GetItemTypeByID(itemTypeId);               
                if (ItemType != null)
                {
                    ViewBag.InventoryItemTypeId = itemTypeId;
                    ViewBag.InventoryItemTypeName = ItemType.Name;
                    ViewBag.IsQuantitative = ItemType.IsQuantitative;
                    ViewBag.AutoGenerateSerial = ItemType.AutoGenerateSerial;
                    ViewBag.WarehouseId = ItemType.WarehouseId;
                    ViewBag.unitId = ItemType.InventoryItemTypeUnitId.HasValue ? ItemType.InventoryItemTypeUnitId.Value : -1;
                    ViewBag.Suppliers = inventoryServices.GetItemtypeVendors(itemTypeId); 
                    ViewBag.CodeType = inventoryServices.GetcodeTypes(); 
                    ViewBag.Warehouses = inventoryServices.GetWarehousesForCreateInvItem(ItemType.WarehouseId);
                    if (ItemType.WarehouseId == 1)
                    {
                        ViewBag.SubWarehouseStructure = inventoryServices.GetSubWarehouseStructure(ItemType.WarehouseId);
                    }                   
                    ViewBag.status = new SelectList(ItemType.InventoryItemStatusInventoryItemTypes.Select(c => c.InventoryItemStatus), "InventoryItemStatusId", "Name");
                    return ItemType.WarehouseId;
                }
                return -1;
            }
            catch (Exception)
            {

                return -1;
            }
        }
        [HttpGet]
        public IActionResult Create(int itemTypeId)
        {
            try
            {
                long WarehouseId = PrepareCreatePageInventoryItem(itemTypeId);
                if (WarehouseId == 1)
                {
                    return PartialView("CreateItemWarehouseSpareparts");
                }
                else if (WarehouseId == 2)
                {
                    ViewBag.TirePatterns = inventoryServices.getValuesTirePattern();
                    ViewBag.TireSizes = inventoryServices.getValuesTireSize();
                    return PartialView("CreateItemWarehouseTires");
                }
                else if (WarehouseId == 3)
                {
                    ViewBag.Viscosity = inventoryServices.getValuesViscosity();
                    return PartialView("CreateItemWarehouseOils");
                }
                return PartialView("CustomError");
            }
            catch (Exception ex)
            {
                return PartialView("CustomError",ex);
            }          
        }

        [HttpPost]
        public IActionResult CreateItemForSpareparts(InventoryItemWarehouseSpareparts model)
        {
            if (ModelState.IsValid)
            {
                string[] SerialNumbers = !string.IsNullOrEmpty(model.SerialNumbers) ? model.SerialNumbers.Split('+') : null;
                var itemType = inventoryServices.GetItemTypeByID(model.InventoryItemTypeId);
                if (itemType.InventoryItemTypeUnitId == 1 && !itemType.AutoGenerateSerial)
                {
                    if (SerialNumbers is null)
                    {
                        PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                        ViewBag.ValMassage = "Please check the quantity and Serial Number";
                        return PartialView("CreateItemWarehouseSpareparts", model);
                    }
                    if (model.Quantity != SerialNumbers.Length)
                    {
                        PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                        ViewBag.ValMassage = "Please check the quantity and Serial Number";
                        return PartialView("CreateItemWarehouseSpareparts", model);
                    }
                }
                ItemTypeViewModel model1 = new ItemTypeViewModel()
                {
                    AutoGenerateSerial = itemType.AutoGenerateSerial,
                    Code = model.CodeTypeId.Value == -1 ? null : model.Code,
                    CodeTypeId = model.CodeTypeId.Value == -1 ? null : model.CodeTypeId.Value,
                    InventoryItemStatusId = model.InventoryItemStatusId,
                    InventoryItemTypeId = model.InventoryItemTypeId,
                    InventoryLocationId = model.InventoryLocationId,
                    IsQuantitative = itemType.IsQuantitative,
                    IssueNumber = model.IssueNumber,
                    Notes = model.Notes,
                    PartNumber = model.PartNumber,
                    Quantity = model.Quantity,
                    WarehouseId = itemType.WarehouseId,
                    SerialNumbers = model.SerialNumbers,
                    UnitCost = model.UnitCost,
                    unitId = itemType.InventoryItemTypeUnitId.HasValue ? itemType.InventoryItemTypeUnitId.Value : -1,
                    VendorId = model.VendorId,
                };
                int status = inventoryServices.AddInventoryItem(model1);
                if (status == 1)
                {
                    return RedirectToAction("ViewItemType", "InventoryItemType", new { id = model.InventoryItemTypeId });
                }

            }
            PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
            return PartialView("CreateItemWarehouseSpareparts", model);
        }

        [HttpPost]
        public IActionResult CreateItemForTires(InventoryItemWarehouseTires model)
        {
            if (ModelState.IsValid)
            {
                string[] SerialNumbers = !string.IsNullOrEmpty(model.SerialNumbers) ? model.SerialNumbers.Split('+') : null;
                var itemType = inventoryServices.GetItemTypeByID(model.InventoryItemTypeId);
                if (itemType.InventoryItemTypeUnitId == 1 && !itemType.AutoGenerateSerial)
                {
                    if (SerialNumbers is null)
                    {
                        ViewBag.TirePatterns = inventoryServices.getValuesTirePattern();
                        ViewBag.TireSizes = inventoryServices.getValuesTireSize();
                        PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                        ViewBag.ValMassage = "Please check the quantity and Serial Number";
                        return PartialView("CreateItemWarehouseTires", model);
                    }
                    if (model.Quantity != SerialNumbers.Length)
                    {
                        ViewBag.TirePatterns = inventoryServices.getValuesTirePattern();
                        ViewBag.TireSizes = inventoryServices.getValuesTireSize();
                        PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                        ViewBag.ValMassage = "Please check the quantity and Serial Number";
                        return PartialView("CreateItemWarehouseTires", model);
                    }
                }
                ItemTypeViewModel model1 = new ItemTypeViewModel()
                {
                    AutoGenerateSerial = itemType.AutoGenerateSerial,
                    Code = model.CodeTypeId.Value == -1 ? null : model.Code,
                    CodeTypeId = model.CodeTypeId.Value == -1 ? null : model.CodeTypeId.Value,
                    InventoryItemStatusId = model.InventoryItemStatusId,
                    InventoryItemTypeId = model.InventoryItemTypeId,                   
                    IsQuantitative = itemType.IsQuantitative,
                    IssueNumber = model.IssueNumber,
                    Notes = model.Notes,                    
                    Quantity = model.Quantity,
                    WarehouseId = itemType.WarehouseId,
                    SerialNumbers = model.SerialNumbers,
                    UnitCost = model.UnitCost,
                    unitId = itemType.InventoryItemTypeUnitId.HasValue ? itemType.InventoryItemTypeUnitId.Value : -1,
                    VendorId = model.VendorId,
                    StandardTreadDepth = model.StandardTreadDepth,
                    Threshold = model.Threshold,
                    TirePattern=model.TirePattern,
                    TireSize = model.TireSize,                    
                };
                int status = inventoryServices.AddInventoryItem(model1);
                if (status == 1)
                {
                    return RedirectToAction("ViewItemType", "InventoryItemType", new { id = model.InventoryItemTypeId });
                }

            }
            ViewBag.TirePatterns = inventoryServices.getValuesTirePattern();
            ViewBag.TireSizes = inventoryServices.getValuesTireSize();
            PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
            return PartialView("CreateItemWarehouseTires", model);
        }

        [HttpPost]
        public IActionResult CreateItemForOils(InventoryItemWarehouseOils model)
        {
            if (ModelState.IsValid)
            {
                string[] SerialNumbers = !string.IsNullOrEmpty(model.SerialNumbers) ? model.SerialNumbers.Split('+') : null;
                var itemType = inventoryServices.GetItemTypeByID(model.InventoryItemTypeId);
                if (itemType.InventoryItemTypeUnitId == 1 && !itemType.AutoGenerateSerial)
                {
                    if (SerialNumbers is null)
                    {
                        ViewBag.Viscosity = inventoryServices.getValuesViscosity();
                        PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                        ViewBag.ValMassage = "Please check the quantity and Serial Number";
                        return PartialView("CreateItemWarehouseOils", model);
                    }
                    if (model.Quantity != SerialNumbers.Length)
                    {
                        ViewBag.Viscosity = inventoryServices.getValuesViscosity();                      
                        PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                        ViewBag.ValMassage = "Please check the quantity and Serial Number";
                        return PartialView("CreateItemWarehouseOils",model);
                    }
                }
                ItemTypeViewModel model1 = new ItemTypeViewModel()
                {
                    AutoGenerateSerial = itemType.AutoGenerateSerial,
                    Code = model.CodeTypeId.Value == -1 ? null : model.Code,
                    CodeTypeId = model.CodeTypeId.Value == -1 ? null : model.CodeTypeId.Value,
                    InventoryItemStatusId = model.InventoryItemStatusId,
                    InventoryItemTypeId = model.InventoryItemTypeId,
                    IsQuantitative = itemType.IsQuantitative,
                    IssueNumber = model.IssueNumber,
                    Notes = model.Notes,
                    Quantity = model.Quantity,
                    WarehouseId = itemType.WarehouseId,
                    SerialNumbers = model.SerialNumbers,
                    UnitCost = model.UnitCost,
                    unitId = itemType.InventoryItemTypeUnitId.HasValue ? itemType.InventoryItemTypeUnitId.Value : -1,
                    VendorId = model.VendorId,
                    Viscosityid=model.Viscosity
                };
                int status = inventoryServices.AddInventoryItem(model1);
                if (status == 1)
                {
                    return RedirectToAction("ViewItemType", "InventoryItemType", new { id = model.InventoryItemTypeId });
                }

            }
            ViewBag.Viscosity = inventoryServices.getValuesViscosity();
            PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
            return PartialView("CreateItemWarehouseOils",model);
        }
        //[HttpPost]
        //public IActionResult Create(ItemTypeViewModel model) 
        //{

        //    if (ModelState.IsValid )
        //    {
        //        if (model.WarehouseId == 1)
        //        {
        //            if (string.IsNullOrEmpty(model.PartNumber))
        //            {
        //                ViewBag.valPartNumber = "Part Number is required";                       
        //              return PartialView("Create", inventoryServices.PrepareCreatePageInventoryItem(model.InventoryItemTypeId, model));
        //            }
        //            if (!model.InventoryLocationId.HasValue)
        //            {
        //                ViewBag.valInventoryLocationId = "Location is required";
        //                return PartialView("Create", inventoryServices.PrepareCreatePageInventoryItem(model.InventoryItemTypeId, model));
        //            }
        //        }
        //        string[] SerialNumbers =  !string.IsNullOrEmpty(model.SerialNumbers) ?model.SerialNumbers.Split('+') : null;
        //        if (model.unitId == 1 && !model.AutoGenerateSerial)
        //        {
        //            if (model.Quantity != SerialNumbers.Length)
        //            {
        //                ViewBag.ValMassage = "Please check the quantity and Serial Number";
        //                return PartialView("Create", model);
        //            }
        //        }               

        //        try
        //        {
        //            //int status = inventoryServices.AddInventoryItem(model);
        //            //if (status == 1)
        //            //{
        //            //    return RedirectToAction("ViewItemType", "InventoryItemType", new { id = model.InventoryItemTypeId });
        //            //}
        //        }
        //        catch (Exception ex)
        //        {
        //            return PartialView("CustomError" ,ex);
        //        }
        //    }
        //    return PartialView("Create", inventoryServices.PrepareCreatePageInventoryItem(model.InventoryItemTypeId, model));
        //}
        [HttpGet]
        public IActionResult Edit(long itemId)
        {
            try
            {
                var ItemType = inventoryServices.GetItemById(itemId);
                long WarehouseId = PrepareCreatePageInventoryItem(ItemType.InventoryItemTypeId);

                if (WarehouseId == 1)
                {
                    InventoryItemWarehouseSpareparts model = inventoryServices.PrepareEditPageInventoryItem(itemId);

                    return PartialView("EditItemWarehouseSpareparts", model);
                }
                else if (WarehouseId == 2)
                {
                    InventoryItemWarehouseTires model = inventoryServices.PrepareEditPageInventoryItemTires(itemId);
                    ViewBag.TirePatterns = inventoryServices.getValuesTirePattern();
                    ViewBag.TireSizes = inventoryServices.getValuesTireSize();
                    return PartialView("EditItemWarehouseTires", model);
                }
                else if (WarehouseId == 3)
                {
                    InventoryItemWarehouseOils model = inventoryServices.PrepareEditPageInventoryItemOils(itemId);

                    ViewBag.Viscosity = inventoryServices.getValuesViscosity();
                    return PartialView("EditItemWarehouseOils", model);
                }

                return PartialView("CustomError");
            }
            catch (Exception ex)
            {

                return PartialView("CustomError",ex);
            }            
        }
        [HttpPost]
        public IActionResult EditItemForSpareparts(InventoryItemWarehouseSpareparts model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var item = inventoryServices.getInventoryItemTypeById(model.InventoryItemTypeId);
                    if (string.IsNullOrEmpty(model.SerialNumbers) && !item.AutoGenerateSerial)
                    {
                        PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                        ViewBag.valSerialNumber = "Please Inter Serial Number";
                        return PartialView("EditItemWarehouseSpareparts", model);
                    }
                    inventoryServices.EditItemForSpareparts(model);
                    //return RedirectToAction("Edit", new { itemId = model.InventoryItemId.Value });
                    return RedirectToAction("Display", new { itemId = model.InventoryItemId.Value });
                }

                //long WarehouseId = PrepareCreatePageInventoryItem(model.InventoryItemTypeId);             
                PrepareCreatePageInventoryItem(model.InventoryItemTypeId);               
                 return PartialView("EditItemWarehouseSpareparts", model);             
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }          
        }
        [HttpPost]
        public IActionResult EditItemForOils(InventoryItemWarehouseOils model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var item = inventoryServices.getInventoryItemTypeById(model.InventoryItemTypeId);
                    if (string.IsNullOrEmpty(model.SerialNumbers) && !item.AutoGenerateSerial)
                    {
                        ViewBag.Viscosity = inventoryServices.getValuesViscosity();
                        PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                        ViewBag.valSerialNumber = "Please Inter Serial Number";
                        return PartialView("EditItemWarehouseOils", model);
                    }
                    inventoryServices.EditItemForOils(model);
                    return RedirectToAction("Display", new { itemId = model.InventoryItemId.Value });
                }

                ViewBag.Viscosity = inventoryServices.getValuesViscosity();
                PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                return PartialView("EditItemWarehouseOils", model);
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        //[HttpPost]
        //public IActionResult Edit(InventoryItem model)
        //{
        //    long item = inventoryServices.EditInventoryItem(model);
        //    return Json(item);
        //}
        [HttpPost]
        public IActionResult EditItemForTires(InventoryItemWarehouseTires model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var item = inventoryServices.getInventoryItemTypeById(model.InventoryItemTypeId);
                    if (string.IsNullOrEmpty(model.SerialNumbers) && !item.AutoGenerateSerial)
                    {
                        ViewBag.TirePatterns = inventoryServices.getValuesTirePattern();
                        ViewBag.TireSizes = inventoryServices.getValuesTireSize();
                        PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                        ViewBag.valSerialNumber = "Please Inter Serial Number";
                        return PartialView("EditItemWarehouseTires", model);
                    }
                    inventoryServices.EditItemForTires(model);                   
                    return RedirectToAction("Display", new { itemId = model.InventoryItemId.Value });
                }

                ViewBag.TirePatterns = inventoryServices.getValuesTirePattern();
                ViewBag.TireSizes = inventoryServices.getValuesTireSize();
                PrepareCreatePageInventoryItem(model.InventoryItemTypeId);
                return PartialView("EditItemWarehouseTires", model);
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        [HttpGet]
        public IActionResult Display(long itemId)
        {
            try
            {

               var item= inventoryServices.GetItemTypeByItemId(itemId);

                if (item.InventoryItemType.Warehouse.WarehouseId == 1)
                {
                    InventoryItemWarehouseSpareparts model = inventoryServices.PrepareDisplayPageInventoryItem(itemId);
                    return PartialView("Display", model);
                }
                else if (item.InventoryItemType.WarehouseId == 2)
                {
                    InventoryItemWarehouseTires model = inventoryServices.PrepareDisplayPageInventoryItemTire(itemId);
                    return PartialView("DisplayTire", model);
                }
                else if (item.InventoryItemType.WarehouseId == 3)
                {
                    InventoryItemWarehouseOils model = inventoryServices.PrepareDisplayPageInventoryItemOil(itemId);
                    return PartialView("DisplayOil", model);
                }

                return PartialView("CustomError");
            }
            catch (Exception ex)
            {

                return PartialView("CustomError", ex);
            }
        }
        [HttpPost]
        public IActionResult IndexPage(int CurrentPageIndex)
        {
            try
            {             
                return PartialView(inventoryServices.getAllInventoryItemPaging(CurrentPageIndex));
            }
            catch (Exception ex)
            {
                return PartialView("CustomError",ex); 
            }          
        }
        [HttpPost]
        public IActionResult Changelength(int CurrentPageIndex , int length)
        {           
            try
            {
                return PartialView("IndexPage", inventoryServices.getAllInventoryItemPagingWithChangelength(CurrentPageIndex, length));
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }

        public IActionResult getModelByBrandID(long brandId)
        {
            try
            {
                return Json(inventoryServices.getModelsByBrandID(brandId));
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        public IActionResult GetLaneWarehouse(long subWarehouseId)
        {
            try
            {
                var structure = inventoryServices.GetLaneWarehouseStructure(subWarehouseId);
                if (structure == null)
                {
                    return Json(new { status = -1 ,@object =""});
                }
                return Json(new { status = 1, @object = structure });
            }
            catch 
            {
                return Json(new { status = -1, @object = "" });
            }
        }

        public IActionResult GetShelfWarehouse(long laneWarehouseId)
        {
            try
            {
                var structure = inventoryServices.GetShelfWarehouseStructure(laneWarehouseId);

                if (structure == null)
                {
                    return Json(new { status = -1, @object = "" });
                }
                return Json(new { status = 1, @object = structure });
            }
            catch 
            {
                return Json(new { status = -1, @object = "" });
            }
    
        }
    }
}
