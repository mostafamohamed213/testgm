using AutoMapper;
using CGARMAN.Services;
using CGARMAN.ViewModel.InventoryViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class InventoryItemTypeController : Controller
    {
        private InventoryServices inventoryServices;
        private readonly IMapper mapper;
        public InventoryItemTypeController(InventoryServices _inventoryServices, IMapper _mapper)
        {
            inventoryServices = _inventoryServices;
            mapper = _mapper;
        }

        [HttpPost]
        public IActionResult IndexPage(int CurrentPageIndex)
        {
            try
            {
                ViewBag.Warehouses = inventoryServices.GetWarehouses();
                return PartialView("_IndexPage", inventoryServices.getAllInventoryItemTypePaging(CurrentPageIndex));
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
                ViewBag.Warehouses = inventoryServices.GetWarehouses();
                return PartialView("_IndexPage", inventoryServices.getAllInventoryItemTypePagingWithChangelength(CurrentPageIndex, length));
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
        }
        [HttpGet]
        public IActionResult ViewItemType(int id)
        {
            try
            {
                return PartialView("_ViewItemType", inventoryServices.getInventoryItemTypeById(id));
            }
            catch (Exception ex)
            {

                return PartialView("CustomError", ex);
            }
                  
        }

        [HttpGet]
        public IActionResult GetItemTypeStatusBrandCodetypeVendorIsQuantityAndIsAutoserial(long itemType)
        {
            var status = inventoryServices.GetItemTypeStatus(itemType);
            //var brands = inventoryServices.GetItemTypeBrands(itemType);
            //var codetypes = inventoryServices.GetItemtypeCodetypes(itemType);
            //var vendors = inventoryServices.GetItemtypeVendors(itemType);
            var IsQuantity = inventoryServices.ItemTypeIsQuantity(itemType);
            var IsAutoGenerateSerial = inventoryServices.ItemTypeIsAutoGenerateSerial(itemType);
            return Json(new { IsAutoGenerateSerial ,IsQuantity, status });
        }
        [HttpGet]
        public IActionResult GetClassificationsCostCenterAndUnits(long warehouseId)
        {
            try
            {
                var classifications = inventoryServices.GetClassifications(warehouseId);
                var units = inventoryServices.GetUnits();
                var costCenters = inventoryServices.CostCenters();
               
                return Json(new { classifications, units, costCenters });

            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
           
        }
        public IActionResult getModelsBybrandid(long id)
        {
            try
            {
                if (id > 0)
                {
                    var models = inventoryServices.getModelsByBrandID(id);
                    if (models != null && models.Count()>0)
                    {
                      return Json(models);
                    }                    
                }
              return Json(0);
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }
           
        }
        
        [HttpGet]
        public IActionResult GetPopupClassificationNotContainWarehouseId(long warehouseId)
        {
            var classificationsNotadded = inventoryServices.GetClassificationsNotContainWarehouseId(warehouseId);
            ViewBag.warehouseId = warehouseId;
            return PartialView("_PopupAddClassification" , classificationsNotadded);

        }
        [HttpGet]
        public IActionResult GetPopupAddBrand(long warehouseId)
        {
            ViewBag.warehouseId = warehouseId;
            return PartialView("_PopupAddBrand");

        }
        [HttpGet]
        public IActionResult GetPopupAddStausToItemType(int id ,string Name)
        {
            try
            {
                var Status = inventoryServices.GetStatusNotContainItemType(id);
                ViewBag.ItemTypeid2 = id;
                ViewBag.ItemTypeName = Name;

                 return PartialView("_addStatusToInventoryItemType", Status);  
            }
            catch 
            {
                return Json(null);
            }          
         
        }
        [HttpPost]
        public IActionResult AssignStatus(string statusValues, int ItemTypeid)
        {
            try
            {
                string[] values = statusValues.Split(',');
                foreach (var item in values)
                {
                    int statusId = int.Parse(item);
                    inventoryServices.AssignStatusToItemType(statusId, ItemTypeid);
                }
                var status= inventoryServices.GetItemTypeStatus(ItemTypeid);              
                //List<string> status1=new List<string>();               
                //foreach (var item in status)
                //{
                //    status1.Add(item.Text);
                //}
                var StatusNotContain= inventoryServices.GetStatusNotContainItemType(ItemTypeid);
                return Json(new { status = ItemTypeid, currentStatus = status , statusNotContain =  new SelectList(StatusNotContain, "InventoryItemStatusId", "Name"), countStatusNotContain = StatusNotContain.Count() });

            }
            catch (Exception ex)
            {
                return Json(new { status = 0, @object = ex });
            }                
        }
        [HttpPost]
        public IActionResult AddStatus(string StatusName,int ItemTypeid)
        {
            try
            {
                int status = inventoryServices.AddStatus(StatusName);              
                if (status > 0)
                {
                    inventoryServices.AssignStatusToItemType(status, ItemTypeid);
                    var itemStatuses = inventoryServices.GetItemTypeStatus(ItemTypeid);
                    //List<string> status1 = new List<string>();
                    //foreach (var item in itemStatuses)
                    //{
                    //    status1.Add(item.Text);
                    //}
                    return Json(new { status = ItemTypeid, @object = itemStatuses });
                }
                if (status == -2)
                {
                    return Json(new { status = -1, @object = "" });
                }
                return Json(new { status = -1, @object = "" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, @object = ex });
            }
        }
        [HttpPost]
        public IActionResult SaveClassification(long WarehouseId, int classificationId)
        {
            if (inventoryServices.AddClassificationToWarehouse(WarehouseId, classificationId) == 1)
            {
                return Json(WarehouseId);
            }
            return Json(0);
        }

        [HttpPost]
        public IActionResult SaveBrand(long WarehouseId, string brandName)
        {
            int status= inventoryServices.AddBrand(brandName);
            var brands =   inventoryServices.GetBrands();
            if (status == 1)
            {
                return Json(new { status = WarehouseId , @object= brands });
            }
            if (status == 2)
            {
                return Json(new { status = -1 , @object = brands } );
            }
            return Json(new { status = -1 , @object = brands });
        }
        [HttpGet]
        public IActionResult GetPopupAddModelByBrandId(long warehouseId ,int brandId ,string brandName)
        {
            ViewBag.warehouseId = warehouseId;
            ViewBag.brandId = brandId;
            ViewBag.brandName = brandName;
            return PartialView("_GetPopupAddModelByBrandId");

        }
        [HttpGet]
        public IActionResult GetPopupAddUnits(long warehouseId)
        {
            ViewBag.warehouseId = warehouseId;        
            return PartialView("_GetPopupAddUnits");

        }
        [HttpGet]
        public IActionResult GetPopupAddCostCenter(long warehouseId)
        {
            ViewBag.warehouseId = warehouseId;           
            return PartialView("_GetPopupAddCostCenter");

        }
        [HttpPost]
        public IActionResult AddModelByBrandId(long warehouseId, int brandId, string modelName)
        {
            int status = inventoryServices.AddModelByBrandId(brandId, modelName);
            var models = inventoryServices.getModelsByBrandID(brandId);
            if (status == 1)
            {
                return Json(new { status = warehouseId, @object = models });
            }
            if (status == 2)
            {
                return Json(new { status = -1, @object = models });
            }
            return Json(new { status = -1, @object = models });
        }

        [HttpPost]
        public IActionResult AddUnit(long warehouseId, string UnitName, string UnitDescription)
        {
            int status = inventoryServices.AddUnit(UnitName, UnitDescription);
            var models = inventoryServices.GetUnits();
            if (status == 1)
            {
                return Json(new { status = warehouseId, @object = models });
            }
            if (status == 2)
            {
                return Json(new { status = -1, @object = models });
            }
            return Json(new { status = -1, @object = models });
        }
        
       [HttpPost]
        public IActionResult AddClassification(long warehouseId, string classificationName)
        {
            int status = inventoryServices.AddClassification(classificationName);
            var classifications = inventoryServices.GetClassificationsNotContainWarehouseId(warehouseId);
            if (status == 1)
            {
                return Json(new { status = warehouseId, @object = classifications });
            }
            if (status == 2)
            {
                return Json(new { status = -1, @object = classifications });
            }
            return Json(new { status = -1, @object = classifications });

        }
        [HttpPost]
        public IActionResult AddCostCenter(long warehouseId, string CostCenterName, string CostCenterValue)
        {
            int status = inventoryServices.AddCostCenter(CostCenterName, CostCenterValue);
            var models = inventoryServices.CostCenters();
            if (status == 1)
            {
                return Json(new { status = warehouseId, @object = models });
            }
            if (status == 2)
            {
                return Json(new { status = -1, @object = models });
            }
            return Json(new { status = -1, @object = models });

        }
        [HttpGet]
        public IActionResult Create()
        {
            //InventoryItemTypeViewModel model = new InventoryItemTypeViewModel();
            //model.CostCenters= inventoryServices.CostCenters();
            //model.Units= inventoryServices.GetUnits();
            //model.Classifications = inventoryServices.GetClassifications(warehouseId);
            ViewBag.warehouses = inventoryServices.GetWarehouses();
            ViewBag.brands = inventoryServices.GetBrands();
            ViewBag.units = inventoryServices.GetUnits();
            ViewBag.costCenters = inventoryServices.CostCenters();         
            return PartialView("Create");

        }

        [HttpPost]
        public IActionResult Create(CreateInventoryItemTypeViewModel model)
        {
            try
            {
                //return RedirectToAction("ViewItemType", new { id = 11 });
                if (!model.AutoGenerateSerial && model.InventoryItemTypeUnitId != 1)
                {
                    ViewBag.warehouses = inventoryServices.GetWarehouses();
                    ViewBag.brands = inventoryServices.GetBrands();
                    ViewBag.units = inventoryServices.GetUnits();
                    ViewBag.costCenters = inventoryServices.CostCenters();
                    if (model.WarehouseId > 0)
                    {
                        ViewBag.classifications = inventoryServices.GetClassifications(model.WarehouseId);
                    }
                    ViewBag.valAutoGenerateSerialAndUnit = "Serial be with unit";
                    return PartialView("Create", model);
                }
                if (ModelState.IsValid)
                {
                    
                    int id = inventoryServices.AddInventoryItemType(model);
                    //if (model.IsComeFromPopup)
                    //{
                    //    return Json(1);
                    //}
                    return RedirectToAction("ViewItemType",new { id = id });
                }
                //if (model.IsComeFromPopup)
                //{
                //    return Json(0);
                //}
                ViewBag.warehouses = inventoryServices.GetWarehouses();
                ViewBag.brands = inventoryServices.GetBrands();
                ViewBag.units = inventoryServices.GetUnits();
                ViewBag.costCenters = inventoryServices.CostCenters();
                if (model.WarehouseId > 0)
                {
                    ViewBag.classifications = inventoryServices.GetClassifications(model.WarehouseId);
                }

                return PartialView("Create", model);
            }
            catch (Exception ex)
            {
                //if (model.IsComeFromPopup)
                //{
                //    return Json(0);
                //}
                return PartialView("CustomError", ex);
            }

        }
        [HttpGet]
        public IActionResult GetClassifications(long warehouseId)
        {
            try
            {
                var classifications = inventoryServices.GetClassifications(warehouseId);
               

                return Json(new { classifications });

            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }

        }
        [HttpGet]
        public IActionResult Edit(int itemTypeId)
        {           
            try
            {
                InventoryItemType itemType = inventoryServices.GetItemTypeByID(itemTypeId);
                ViewBag.warehouses = inventoryServices.GetWarehouses();
                ViewBag.brands = inventoryServices.GetBrands();
                ViewBag.classifications = inventoryServices.GetClassifications(itemType.WarehouseId);
                ViewBag.units = inventoryServices.GetUnits();
                ViewBag.costCenters = inventoryServices.CostCenters();
                ViewBag.models = inventoryServices.getModelsByBrandID(itemType.BrandId);
                var itemTypeViewModel = mapper.Map<CreateInventoryItemTypeViewModel>(itemType);
                itemTypeViewModel.UrlImage = itemType.PathImage;
                ViewBag.hasItems = inventoryServices.CheckIfInventoryItemTypeHasItems(itemTypeId);
                return PartialView("_Edit", itemTypeViewModel);
            }
            catch (Exception ex)
            {
                return PartialView("CustomError", ex);
            }

        }

        [HttpPost]
        public IActionResult Edit(CreateInventoryItemTypeViewModel model)
        {
            try
            {
                if (!model.AutoGenerateSerial && model.InventoryItemTypeUnitId != 1)
                {
                    ViewBag.warehouses = inventoryServices.GetWarehouses();
                    ViewBag.brands = inventoryServices.GetBrands();
                    ViewBag.classifications = inventoryServices.GetClassifications(model.WarehouseId);
                    ViewBag.units = inventoryServices.GetUnits();
                    ViewBag.costCenters = inventoryServices.CostCenters();
                    ViewBag.models = inventoryServices.getModelsByBrandID(model.BrandId);
                    ViewBag.hasItems = inventoryServices.CheckIfInventoryItemTypeHasItems(model.InventoryItemTypeId);
                    ViewBag.valAutoGenerateSerialAndUnit = "Serial be with unit";
                    return PartialView("_Edit", model);
                }
                if (ModelState.IsValid)
                {
                  bool hasItems=  inventoryServices.CheckIfInventoryItemTypeHasItems(model.InventoryItemTypeId);
                    int id=inventoryServices.EditInventoryItemType(model, hasItems);
                    return RedirectToAction("ViewItemType", new { id = id });
                }
              
                ViewBag.warehouses = inventoryServices.GetWarehouses();
                ViewBag.brands = inventoryServices.GetBrands();
                ViewBag.classifications = inventoryServices.GetClassifications(model.WarehouseId);
                ViewBag.units = inventoryServices.GetUnits();
                ViewBag.costCenters = inventoryServices.CostCenters();
                ViewBag.models = inventoryServices.getModelsByBrandID(model.BrandId);
                ViewBag.hasItems = inventoryServices.CheckIfInventoryItemTypeHasItems(model.InventoryItemTypeId);
                return PartialView("_Edit", model);
            }
            catch (Exception ex)
            {
               
                return PartialView("CustomError", ex);
            }

        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            try
            {
                var values = inventoryServices.AutoCompleteInventoryItemType(prefix);               
                return Json(values);
            }
            catch (Exception)
            {              
                return Json(0);
            }
         
        }
        [HttpPost]
        public IActionResult Search(int elementID , string label ,long WarehousesId)
        {
            try
            {      
                
                var values = inventoryServices.SearchInventoryItemType(elementID,label,WarehousesId);
                return PartialView("_Search", values);
            }
            catch (Exception)
            {
                return PartialView("CustomError");
            }

        }

        [HttpPost]
        public IActionResult DeleteRelationInventoryItemTypeAndStatus(int statusId,int itemTypeId)
        {             
            try
            {
                int status = inventoryServices.DeleteRelationInventoryItemTypeAndStatus(statusId, itemTypeId);               
                if (status == 1)
                {
                    var status1 = inventoryServices.GetItemTypeStatus(itemTypeId);
                    var StatusNotContain = inventoryServices.GetStatusNotContainItemType(itemTypeId);
                    return Json(new { status = itemTypeId, currentStatus = status1, statusNotContain = new SelectList(StatusNotContain, "InventoryItemStatusId", "Name"), countStatusNotContain = StatusNotContain.Count() });
                }
                else if (status == 0)
                {
                    return Json(new { status = 0});
                }
                else if (status == -1)
                {
                   return Json(new { status = -1 });
                }

                return Json(new { status = -1 });
            }
            catch (Exception)
            {
                return Json(new { status = -1 });
            }         

        }
    }
}
