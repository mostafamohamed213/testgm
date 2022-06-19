using AutoMapper;
using CGARMAN.ViewModel;
using CGARMAN.ViewModel.InventoryViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Services
{
    public class InventoryServices
    {
        IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public InventoryServices(IUnitOfWork _unitOfWork , IMapper _mapper, IWebHostEnvironment hostEnvironment)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            webHostEnvironment = hostEnvironment;
        }

        internal SelectList GetcodeTypes()
        {
            var codeTypes = unitOfWork.CodeTypes.GetAll();
            if (codeTypes != null && codeTypes.Count() > 0)
            {
                return new SelectList(codeTypes, "CodeTypeId", "Name");
            }
            return null;
        }
        internal InventoryItem GetItemById(long itemId)
        {
           var item= unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == itemId,new[] { "CodeType", "InventoryItemStatus", "InventoryItemType" , "InventoryLocation" , "Location" , "Vendor" });
            return item;
        }
        internal int AddCodeType(string codeTypeName)
        {
           
                CodeType codeType = new CodeType() { Name = codeTypeName.Trim() };
                var check = unitOfWork.CodeTypes.GetOne(c => c.Name.Trim().ToLower() == codeTypeName.Trim().ToLower());
                if (check == null)
                {
                    unitOfWork.CodeTypes.Add(codeType);                
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 14,
                        SystemUserID = "1",
                        ObjectID = codeType.CodeTypeId,
                        Object1 = $"CodeTypeId = {codeType.CodeTypeId} ~ Name = {codeType.Name}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();

                }
                else
                {
                    return -1;
                }

                return 1;
            
        }

    
        internal InventoryItemStatus getStatusById(int id)
        {
            var status= unitOfWork.InventoryItemStatus.GetOne(c => c.InventoryItemStatusId == id);
            if (status is null)
            {
                return null;
            }
            return status;
        }

        internal Warehouse GetWarehouseByid(long  warehouseId)
        {
          return  unitOfWork.Warehouse.GetOne(c => c.WarehouseId == warehouseId);
        }

        internal InventoryItemTypeUnit getUnitById(int id)
        {
            var status = unitOfWork.InventoryItemTypeUnits.GetOne(c => c.InventoryItemTypeUnitId == id);
            if (status is null)
            {
                return null;
            }
            return status;
        }

        internal long EditInventoryItem(InventoryItem model)
        {
            try
            {
                var item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == model.InventoryItemId);
                InventoryLog inventoryLog = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 12,
                    SystemUserID = "1",
                    ObjectID = item.InventoryItemId,
                    Object1 = $"InventoryItemId = {item.InventoryItemId} ~ SerialNumber = {item.SerialNumber.Trim()}",
                    Object2 = $"InventoryItemId = {model.InventoryItemId} ~ SerialNumber = {model.SerialNumber.Trim()}",
                    Description = "updated SerialNumber"
                };
                unitOfWork.InventoryLog.Add(inventoryLog);
                item.SerialNumber = model.SerialNumber.Trim();
                unitOfWork.Complete();
                return item.InventoryItemTypeId;
            }
            catch (Exception)
            {

                return 0;
            }
        
        }

        internal InventoryItemTypeClassification getClassificationById(int id)
        {
            var Classification = unitOfWork.InventoryItemTypeClassifications.GetOne(c => c.InventoryItemTypeClassificationId == id,new[] { "WarehouseInventoryItemTypeClassifications" });
            if (Classification is null)
            {
                return null;
            }
            return Classification;
        }
        internal Model getModelById(int id)
        {
            var model = unitOfWork.Models.GetOne(c => c.ModelId == id,new []{ "Brand" });
            if (model is null)
            {
                return null;
            }
            return model;
        }
        internal Brand getBrandById(int id)
        {
            var brand = unitOfWork.Brands.GetOne(c => c.BrandId == id);
            if (brand is null)
            {
                return null;
            }
            return brand;
        }
        internal Vendor getVendorById(int id)
        {
            var Vendor = unitOfWork.Vendors.GetOne(c => c.VendorId == id);
            if (Vendor is null)
            {
                return null;
            }
            return Vendor;
        }

        internal int addVendor(string vendorName)
        {   
                Vendor vendor = new Vendor() { Name = vendorName.Trim() , CreateDts =DateTime.Now };
                var check = unitOfWork.Vendors.GetOne(c => c.Name.Trim().ToLower() == vendorName.Trim().ToLower());
                if (check == null)
                {
                    unitOfWork.Vendors.Add(vendor);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 13,
                        SystemUserID = "1",
                        ObjectID = vendor.VendorId,
                        Object1 = $"VendorId = {vendor.VendorId} ~ Name = {vendor.Name}  ~ CreateDts = {vendor.CreateDts}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return -1;
                }

                return 1;
            
        }

        internal void UpdateStatus(InventoryItemStatus status)
        {
            InventoryItemStatus S =  unitOfWork.InventoryItemStatus.GetOne(C => C.InventoryItemStatusId == status.InventoryItemStatusId);
            if (S is not null)
            {
                InventoryLog inventoryLog = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 7,
                    SystemUserID = "1",
                    ObjectID = S.InventoryItemStatusId,
                    Object1 = $"InventoryItemStatusId = {S.InventoryItemStatusId} ~ Name = {S.Name.Trim()}",
                    Object2 = $"InventoryItemStatusId = {status.InventoryItemStatusId} ~ Name = {status.Name.Trim()}",
                };
                unitOfWork.InventoryLog.Add(inventoryLog);
                S.Name = status.Name.Trim();
                unitOfWork.Complete();
            }
        }

        internal InventoryItemWarehouseSpareparts PrepareEditPageInventoryItem(long itemId)
        {
            var item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == itemId, new[] {"InventoryLocation", "InventoryLocation.ParentInventoryLocation"}); ;
          
            InventoryItemWarehouseSpareparts model = mapper.Map<InventoryItemWarehouseSpareparts>(item);
            model.InventoryItemTypeId = item.InventoryItemTypeId;
            model.SerialNumbers = item.SerialNumber;
            model.LaneId = item.InventoryLocation.ParentInventoryLocationId;
            model.SubWarehouseId = item.InventoryLocation.ParentInventoryLocation.ParentInventoryLocationId;
            model.PartNumber = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 300).Value;
            model.CreateDT = item.CreateDts;
            model.Code = item.Code;
            return model;
        }
        internal InventoryItemWarehouseTires PrepareEditPageInventoryItemTires(long itemId)
        {
            var item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == itemId, new[] { "InventoryLocation", "InventoryLocation.ParentInventoryLocation" }); ;

            InventoryItemWarehouseTires model = mapper.Map<InventoryItemWarehouseTires>(item);
            model.InventoryItemTypeId = item.InventoryItemTypeId;
            model.SerialNumbers = item.SerialNumber;
            var threshold = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 203);
            model.Threshold =  threshold != null ? int.Parse(threshold.Value) : null;
            model.TirePattern = int.Parse(unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 201).Value);
            model.TireSize = int.Parse(unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 202).Value);
            model.StandardTreadDepth = int.Parse(unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 200).Value);
            model.CreateDT = item.CreateDts;

            return model;
        }
        internal InventoryItemWarehouseOils PrepareEditPageInventoryItemOils(long itemId)
        {
            var item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == itemId, new[] { "InventoryLocation", "InventoryLocation.ParentInventoryLocation" }); ;

            InventoryItemWarehouseOils model = mapper.Map<InventoryItemWarehouseOils>(item);
            model.InventoryItemTypeId = item.InventoryItemTypeId;
            model.SerialNumbers = item.SerialNumber;
            model.Viscosity = int.Parse(unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 100).Value);
            model.CreateDT = item.CreateDts;
            return model;
        }
        internal InventoryItemWarehouseSpareparts PrepareDisplayPageInventoryItem(long itemId)
        {
            var item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == itemId, new[] { "CodeType", "InventoryItemStatus", "InventoryItemType", "Vendor", "Location", "InventoryLocation", "InventoryLocation.ParentInventoryLocation", "InventoryLocation.ParentInventoryLocation.ParentInventoryLocation", "InventoryLocation.Warehouse", "InventoryItemType.Warehouse" }); ;

            InventoryItemWarehouseSpareparts model = mapper.Map<InventoryItemWarehouseSpareparts>(item);
            model.SerialNumbers = item.SerialNumber;           
            model.PartNumber = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 300).Value;
            model.display = item;
            return model;
        }
        internal InventoryItemWarehouseOils PrepareDisplayPageInventoryItemOil(long itemId)
        {
            var item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == itemId, new[] { "CodeType", "InventoryItemStatus", "InventoryItemType", "Vendor", "Location", "InventoryLocation", "InventoryLocation.ParentInventoryLocation", "InventoryLocation.ParentInventoryLocation.ParentInventoryLocation", "InventoryLocation.Warehouse", "InventoryItemType.Warehouse" }); ;

            InventoryItemWarehouseOils model = mapper.Map<InventoryItemWarehouseOils>(item);
            model.SerialNumbers = item.SerialNumber;
            var ViscosityVal = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 100).Value;
            model.ViscosityStr = unitOfWork.ParameterListValues.GetOne(c => c.ParameterListValueId == long.Parse(ViscosityVal) ).Value;
            model.display = item;
            return model;
        }
        internal InventoryItemWarehouseTires PrepareDisplayPageInventoryItemTire(long itemId)
        {
            var item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == itemId, new[] { "CodeType", "InventoryItemStatus", "InventoryItemType", "Vendor", "Location", "InventoryLocation", "InventoryLocation.ParentInventoryLocation", "InventoryLocation.ParentInventoryLocation.ParentInventoryLocation", "InventoryLocation.Warehouse", "InventoryItemType.Warehouse" }); ;

            InventoryItemWarehouseTires model = mapper.Map<InventoryItemWarehouseTires>(item);
            model.SerialNumbers = item.SerialNumber;
            var TSizeVal = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 202).Value;
            model.TireSizeStr = unitOfWork.TireSizes.GetOne(c => c.TireSizeId == int.Parse(TSizeVal)).Name;

            var TPatternVal = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 201).Value;
            model.TirePatternStr = unitOfWork.ParameterListValues.GetOne(c => c.ParameterListValueId == long.Parse(TPatternVal)).Value; 

            model.StandardTreadDepthStr = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 200).Value;
           var th = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == item.InventoryItemId && c.ParameterId == 203);
            model.ThresholdStr = th is not null ? th.Value : "Empty";

            model.display = item;

            return model;
        }

        internal void EditItemForSpareparts(InventoryItemWarehouseSpareparts model)
        {
            InventoryItem item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == model.InventoryItemId);
            ParameterValue partNumber = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == model.InventoryItemId && c.ParameterId == 300);
            InventoryLog inventoryLog = new InventoryLog()
            {
                CreateDT = DateTime.Now,
                InventoryLogOperationID = 2,
                InventoryLogTableID = 12,
                SystemUserID = "1",
                ObjectID = item.InventoryItemId,
                Object1 = $"Old : InventoryLocationId = {item.InventoryLocationId} ~ UnitCost = {item.UnitCost}  ~ InventoryItemStatusId = {item.InventoryItemStatusId}  ~ IssueNumber = {item.IssueNumber}  ~ CodeTypeId = {item.CodeTypeId}  ~ Code = {item.Code}  ~ VendorId = {item.VendorId}  ~ SerialNumber = {item.SerialNumber}  ~ Notes = {item.Notes}  ~ partNumber = {partNumber.Value}",
                Object2 = $"New : InventoryLocationId = {model.InventoryLocationId} ~ UnitCost = {model.UnitCost}  ~ InventoryItemStatusId = {model.InventoryItemStatusId}  ~ IssueNumber = {model.IssueNumber}  ~ CodeTypeId = {model.CodeTypeId}  ~ Code = {model.Code}  ~ VendorId = {model.VendorId}  ~ SerialNumber = {model.SerialNumbers}  ~ Notes = {model.Notes}  ~ partNumber = {model.PartNumber}",
            };
            InventoryLog inventoryLogPartNumber = null;
            if (model.PartNumber.ToString().ToLower().Trim() != partNumber.Value.ToLower().Trim())
            {
                 inventoryLogPartNumber = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = partNumber.ParameterValueId,
                    Object1 = $"Old : ParameterValueId = {partNumber.ParameterValueId} ~ Value = {partNumber.Value}",
                    Object2 = $"New : ParameterValueId = {partNumber.ParameterValueId} ~ Value = {model.PartNumber}",
                };
            }
             
            item.InventoryLocationId = model.InventoryLocationId;
            item.UnitCost = model.UnitCost;
            item.InventoryItemStatusId = model.InventoryItemStatusId;           
            item.IssueNumber = model.IssueNumber;
            item.CodeTypeId = model.CodeTypeId.Value == -1 || string.IsNullOrEmpty(model.Code) ? null: model.CodeTypeId.Value;
            item.Code = model.CodeTypeId.Value == -1 || string.IsNullOrEmpty(model.Code) ? null : model.Code;
            item.VendorId = model.VendorId;            
            item.SerialNumber = model.SerialNumbers;
            item.Notes = model.Notes;
            item.CreateDts = model.CreateDT;

            partNumber.Value = model.PartNumber;
            unitOfWork.Complete();           
            unitOfWork.InventoryLog.Add(inventoryLog);
            if (inventoryLogPartNumber is not null)
            {
                unitOfWork.InventoryLog.Add(inventoryLogPartNumber);
                unitOfWork.Complete();
            }
            
        }
        internal void EditItemForOils(InventoryItemWarehouseOils model)
        {
            InventoryItem item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == model.InventoryItemId);
            ParameterValue viscosity = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == model.InventoryItemId && c.ParameterId == 100);
            InventoryLog inventoryLog = new InventoryLog()
            {
                CreateDT = DateTime.Now,
                InventoryLogOperationID = 2,
                InventoryLogTableID = 12,
                SystemUserID = "1",
                ObjectID = item.InventoryItemId,
                Object1 = $"Old :  UnitCost = {item.UnitCost}  ~ InventoryItemStatusId = {item.InventoryItemStatusId}  ~ IssueNumber = {item.IssueNumber}  ~ CodeTypeId = {item.CodeTypeId}  ~ Code = {item.Code}  ~ VendorId = {item.VendorId}  ~ SerialNumber = {item.SerialNumber}  ~ Notes = {item.Notes}  ~ Viscosity = {viscosity.Value}",
                Object2 = $"New :  UnitCost = {model.UnitCost}  ~ InventoryItemStatusId = {model.InventoryItemStatusId}  ~ IssueNumber = {model.IssueNumber}  ~ CodeTypeId = {model.CodeTypeId}  ~ Code = {model.Code}  ~ VendorId = {model.VendorId}  ~ SerialNumber = {model.SerialNumbers}  ~ Notes = {model.Notes}  ~ Viscosity = {model.Viscosity}",
            };
            InventoryLog inventoryLogViscosity = null;
            if (model.Viscosity.ToString().ToLower().Trim() != viscosity.Value.ToLower().Trim())
            {
                inventoryLogViscosity = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = viscosity.ParameterValueId,
                    Object1 = $"Old : ParameterValueId = {viscosity.ParameterValueId} ~ Value = {viscosity.Value}",
                    Object2 = $"New : ParameterValueId = {viscosity.ParameterValueId} ~ Value = {model.Viscosity}",
                };
            }

            item.UnitCost = model.UnitCost;
            item.InventoryItemStatusId = model.InventoryItemStatusId;
            item.IssueNumber = model.IssueNumber;
            item.CodeTypeId = model.CodeTypeId.Value == -1 || string.IsNullOrEmpty(model.Code) ? null : model.CodeTypeId.Value;
            item.Code = model.CodeTypeId.Value == -1 || string.IsNullOrEmpty(model.Code) ? null : model.Code;
            item.VendorId = model.VendorId;
            item.SerialNumber = model.SerialNumbers;
            item.Notes = model.Notes;
            item.CreateDts = model.CreateDT;

            viscosity.Value = model.Viscosity.ToString().Trim();
            unitOfWork.Complete();
            unitOfWork.InventoryLog.Add(inventoryLog);
            if (inventoryLogViscosity is not null)
            {
                unitOfWork.InventoryLog.Add(inventoryLogViscosity);
                unitOfWork.Complete();
            }

        }

        internal void EditItemForTires(InventoryItemWarehouseTires model)
        {
            InventoryItem item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == model.InventoryItemId);
            ParameterValue Threshold = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == model.InventoryItemId && c.ParameterId == 203);
            ParameterValue TireSize = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == model.InventoryItemId && c.ParameterId == 202);
            ParameterValue TirePattern = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == model.InventoryItemId && c.ParameterId == 201);
            ParameterValue StandardTreadDepth = unitOfWork.ParameterValue.GetOne(c => c.ObjectId == model.InventoryItemId && c.ParameterId == 200);

            InventoryLog inventoryLog = new InventoryLog()
            {
                CreateDT = DateTime.Now,
                InventoryLogOperationID = 2,
                InventoryLogTableID = 12,
                SystemUserID = "1",
                ObjectID = item.InventoryItemId,
                Object1 = $"Old : UnitCost = {item.UnitCost}  ~ InventoryItemStatusId = {item.InventoryItemStatusId}  ~ IssueNumber = {item.IssueNumber}  ~ CodeTypeId = {item.CodeTypeId}  ~ Code = {item.Code}  ~ VendorId = {item.VendorId}  ~ SerialNumber = {item.SerialNumber}  ~ Notes = {item.Notes} ~ StandardTreadDepth = {StandardTreadDepth.Value} ~ TirePattern = {TirePattern.Value} ~ TireSize = {TireSize.Value} " +( Threshold != null ? "~ Threshold = " + Threshold.Value : ""),
                Object2 = $"New : UnitCost = {model.UnitCost}  ~ InventoryItemStatusId = {model.InventoryItemStatusId}  ~ IssueNumber = {model.IssueNumber}  ~ CodeTypeId = {model.CodeTypeId}  ~ Code = {model.Code}  ~ VendorId = {model.VendorId}  ~ SerialNumber = {model.SerialNumbers}  ~ Notes = {model.Notes}  ~ StandardTreadDepth = {model.StandardTreadDepth} ~ TirePattern = {model.TirePattern} ~ TireSize = {model.TireSize}" +( model.Threshold.HasValue? "~ Threshold = "+ model.Threshold.Value:""),
            };
            InventoryLog inventoryLogTireSize = null;
            if (model.TireSize.ToString().ToLower().Trim() != TireSize.Value.ToLower().Trim())
            {
                inventoryLogTireSize = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = TireSize.ParameterValueId,
                    Object1 = $"Old : ParameterValueId = {TireSize.ParameterValueId} ~ Value = {TireSize.Value}",
                    Object2 = $"New : ParameterValueId = {TireSize.ParameterValueId} ~ Value = {model.TireSize}",
                };

            }
            InventoryLog inventoryLogTirePattern = null;
            if (model.TirePattern.ToString().ToLower().Trim() != TirePattern.Value.ToLower().Trim())
            {
                 inventoryLogTirePattern = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = TirePattern.ParameterValueId,
                    Object1 = $"Old : ParameterValueId = {TirePattern.ParameterValueId} ~ Value = {TirePattern.Value}",
                    Object2 = $"New : ParameterValueId = {TirePattern.ParameterValueId} ~ Value = {model.TirePattern}",
                };
            }
            InventoryLog inventoryLogStandardTreadDepth = null;
            if ( model.StandardTreadDepth.ToString().ToLower().Trim() != StandardTreadDepth.Value.ToLower().Trim())
            {
                 inventoryLogStandardTreadDepth = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = StandardTreadDepth.ParameterValueId,
                    Object1 = $"Old : ParameterValueId = {StandardTreadDepth.ParameterValueId} ~ Value = {StandardTreadDepth.Value}",
                    Object2 = $"New : ParameterValueId = {StandardTreadDepth.ParameterValueId} ~ Value = {model.StandardTreadDepth}",
                };
            }
            
            InventoryLog inventoryLogThreshold = null;
            if (model.Threshold.HasValue && Threshold !=null && model.Threshold.Value.ToString().ToLower().Trim() != Threshold.Value.ToLower().Trim())
            {
                 inventoryLogThreshold = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = Threshold.ParameterValueId,
                    Object1 = $"Old : ParameterValueId = {Threshold.ParameterValueId} ~ Value = {Threshold.Value}",
                    Object2 = $"New : ParameterValueId = {Threshold.ParameterValueId} ~ Value = {model.Threshold}",
                };
            }else if (!model.Threshold.HasValue && Threshold != null)
            {
                inventoryLogThreshold = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 3,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = Threshold.ParameterValueId,
                    Object2 = $"New : ParameterValueId = {Threshold.ParameterValueId} ~ ObjectId = {Threshold.ObjectId} ~ Value = {Threshold.Value}",
                };
            }
            else if (model.Threshold.HasValue && Threshold == null)
            {
               Threshold=   new ParameterValue()
                {
                    ParameterId=203,
                    ObjectId=item.InventoryItemId,
                    Value = model.Threshold.Value.ToString().Trim(),
                    
                };
                unitOfWork.ParameterValue.Add(Threshold);
                unitOfWork.Complete();
                inventoryLogThreshold = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 1,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = Threshold.ParameterValueId,
                    Object2 = $"New : ParameterValueId = {Threshold.ParameterValueId} ~ ObjectId = {Threshold.ObjectId} ~ Value = {model.Threshold}",
                };
            }

            item.UnitCost = model.UnitCost;
            item.InventoryItemStatusId = model.InventoryItemStatusId;
            item.IssueNumber = model.IssueNumber;
            item.CodeTypeId = model.CodeTypeId.Value == -1  || string.IsNullOrEmpty(model.Code)? null : model.CodeTypeId.Value;
            item.Code = model.CodeTypeId.Value == -1 || string.IsNullOrEmpty(model.Code) ? null : model.Code;
            item.VendorId = model.VendorId;
            item.SerialNumber = model.SerialNumbers;
            item.Notes = model.Notes;
            item.CreateDts = model.CreateDT;

            if (model.Threshold.HasValue && Threshold != null && model.Threshold.Value.ToString().ToLower().Trim() != Threshold.Value.ToLower().Trim())
            {
                Threshold.Value = model.Threshold.Value.ToString();
            }
            else if (!model.Threshold.HasValue && Threshold != null)
            {
                unitOfWork.ParameterValue.Delete(Threshold);
            }

            TirePattern.Value = model.TirePattern.ToString().Trim();
            TireSize.Value = model.TireSize.ToString().Trim();
            StandardTreadDepth.Value = model.StandardTreadDepth.ToString().Trim();
            unitOfWork.Complete();
            unitOfWork.InventoryLog.Add(inventoryLog);

            if (inventoryLogThreshold is not null)
            {
                unitOfWork.InventoryLog.Add(inventoryLogThreshold);
            }
            if (inventoryLogStandardTreadDepth is not null)
            {
                unitOfWork.InventoryLog.Add(inventoryLogStandardTreadDepth);
            }
            if (inventoryLogTirePattern is not null)
            {
                unitOfWork.InventoryLog.Add(inventoryLogTirePattern);
            }
            if (inventoryLogTireSize is not null)
            {
                unitOfWork.InventoryLog.Add(inventoryLogTireSize);
            }

            unitOfWork.Complete();
        }

        internal InventoryItem GetItemTypeByItemId(long itemId)
        {
            var item = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == itemId, new[] {  "InventoryItemType", "InventoryItemType.Warehouse" });
            return item;
        }

        internal int UpdateBrand(Brand brand)
        {
            Brand _brand = unitOfWork.Brands.GetOne(C => C.BrandId == brand.BrandId);
            if (_brand is not null)
            {
                Brand IsExist = unitOfWork.Brands.GetOne(C => C.Name.ToLower().Trim() == _brand.Name.ToLower().Trim() && C.BrandId != _brand.BrandId);
                if (IsExist is not null)
                {
                    return -1;
                }
                InventoryLog inventoryLog = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 10,
                    SystemUserID = "1",
                    ObjectID = _brand.BrandId,
                    Object1 = $"BrandId = {_brand.BrandId} ~ Name = {_brand.Name}",
                    Object2 = $"BrandId = {brand.BrandId} ~ Name = {brand.Name}",                    
                };
                _brand.Name = brand.Name.Trim();
                unitOfWork.InventoryLog.Add(inventoryLog);
                unitOfWork.Complete();
              
                return 1;
            }
            return 0;
        }
        internal int UpdateModel(Model model)
        {
            Model _model = unitOfWork.Models.GetOne(C => C.ModelId == model.ModelId);
            if (_model is not null)
            {
                Model IsExist = unitOfWork.Models.GetOne(C => C.Name.ToLower().Trim() == model.Name.ToLower().Trim() && C.ModelId != model.ModelId && C.BrandId == model.BrandId);
                if (IsExist is not null)
                {
                    return -1;
                }
                InventoryLog inventoryLog = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 11,
                    SystemUserID = "1",
                    ObjectID = _model.ModelId,
                    Object1 = $"ModelId = {_model.ModelId} ~ Name = {_model.Name} ~ BrandId = {_model.BrandId}",
                    Object2 = $"ModelId = {model.ModelId} ~ Name = {model.Name} ~ BrandId = {model.BrandId}",
                };
                unitOfWork.InventoryLog.Add(inventoryLog);
                _model.Name = model.Name.Trim();
                _model.BrandId = model.BrandId;
                unitOfWork.Complete();
                return 1;
            }
            return 0;
        }
        internal int UpdateUnit(InventoryItemTypeUnit unit)
        {
            InventoryItemTypeUnit _unit = unitOfWork.InventoryItemTypeUnits.GetOne(C => C.InventoryItemTypeUnitId == unit.InventoryItemTypeUnitId);
            if (_unit is not null)
            {
                InventoryItemTypeUnit IsExist = unitOfWork.InventoryItemTypeUnits.GetOne(C => C.Name.ToLower().Trim() == unit.Name.ToLower().Trim() && C.InventoryItemTypeUnitId != unit.InventoryItemTypeUnitId);
                if (IsExist is not null)
                {
                    return -1;
                }
                InventoryLog inventoryLog = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 9,
                    SystemUserID = "1",
                    ObjectID = _unit.InventoryItemTypeUnitId,
                    Object1 = $"InventoryItemTypeUnitId = {_unit.InventoryItemTypeUnitId} ~ Name = {_unit.Name.Trim()} ~ Description = {_unit.Description.Trim()}",
                    Object2 = $"InventoryItemTypeUnitId = {unit.InventoryItemTypeUnitId} ~ Name = {unit.Name.Trim()} ~ Description = {unit.Description.Trim()}",
                };
                unitOfWork.InventoryLog.Add(inventoryLog);
                _unit.Name = unit.Name.Trim();
                _unit.Description = unit.Description.Trim();
                unitOfWork.Complete();
                return 1;
            }
            return 0;
        }
        internal int UpdateClassification(InventoryItemTypeClassification classification, long warehouseId)
        {
            InventoryItemTypeClassification _classification = unitOfWork.InventoryItemTypeClassifications.GetOne(C => C.InventoryItemTypeClassificationId == classification.InventoryItemTypeClassificationId);
            if (_classification is not null)
            {
                InventoryItemTypeClassification IsExist = unitOfWork.InventoryItemTypeClassifications.GetOne(C => C.Name.ToLower().Trim() == classification.Name.ToLower().Trim() && C.InventoryItemTypeClassificationId != classification.InventoryItemTypeClassificationId);
                if (IsExist is not null)
                {
                    return -1;
                }
                InventoryLog inventoryLog = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 4,
                    SystemUserID = "1",
                    ObjectID = _classification.InventoryItemTypeClassificationId,
                    Object1 = $"InventoryItemTypeClassificationId = {_classification.InventoryItemTypeClassificationId} ~ Name = {_classification.Name}",
                    Object2 = $"InventoryItemTypeClassificationId = {classification.InventoryItemTypeClassificationId} ~ Name = {classification.Name}",
                };
                unitOfWork.InventoryLog.Add(inventoryLog);
                _classification.Name = classification.Name.Trim();               
                unitOfWork.Complete();
                return 1;
            }
            return 0;
        }

        internal InventoryItemType GetItemTypeByID(int itemTypeId)
        {
            return unitOfWork.InventoryItemType.GetOne(c=>c.InventoryItemTypeId == itemTypeId,new[] { "InventoryItemStatusInventoryItemTypes.InventoryItemStatus" });
        }

        internal int UpdateVendor(Vendor vendor)
        {
            Vendor _vendor = unitOfWork.Vendors.GetOne(C => C.VendorId == vendor.VendorId);
            if (_vendor is not null)
            {
                Vendor IsExist = unitOfWork.Vendors.GetOne(C => C.Name.ToLower().Trim() == vendor.Name.ToLower().Trim() && C.VendorId != vendor.VendorId);
                if (IsExist is not null)
                {
                    return -1;
                }
                InventoryLog inventoryLog = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 13,
                    SystemUserID = "1",
                    ObjectID = _vendor.VendorId,
                    Object1 = $"VendorId = {_vendor.VendorId} ~ Name = {_vendor.Name}  ",
                    Object2 = $"VendorId = {vendor.VendorId} ~ Name = {vendor.Name} ",
                };
                unitOfWork.InventoryLog.Add(inventoryLog);               
                _vendor.Name = vendor.Name.Trim();               
                unitOfWork.Complete();
                return 1;
            }
            return 0;
        }

        

        internal int EditInventoryItemType(CreateInventoryItemTypeViewModel model ,bool hasItems )
        {          
            var itemType = unitOfWork.InventoryItemType.GetOne(c=>c.InventoryItemTypeId == model.InventoryItemTypeId);
            InventoryLog inventoryLog = new InventoryLog()
            {
                CreateDT = DateTime.Now,
                InventoryLogOperationID = 2,
                InventoryLogTableID = 6,
                SystemUserID = "1",
                ObjectID = itemType.InventoryItemTypeId,
                Object1 = $"Old [ InventoryItemTypeId = {itemType.InventoryItemTypeId} ~ Name = {itemType.Name} ~ ModelId = { (itemType.ModelId.HasValue ? itemType.ModelId.Value : "null")} ~ InventoryItemTypeClassificationId = {itemType.InventoryItemTypeClassificationId}  ~ BrandId = {itemType.BrandId} ~ CostCenterId = {itemType.CostCenterId} ~ InventoryItemTypeUnitId = {(itemType.InventoryItemTypeUnitId.HasValue ? itemType.InventoryItemTypeUnitId.HasValue : "null") } ~ Description = {(string.IsNullOrEmpty(itemType.Description) ? itemType.Description : "null")}  ~ WarehouseId = {itemType.WarehouseId}  ~ AutoGenerateSerial = {itemType.AutoGenerateSerial}]",
                Object2 = $"New [ InventoryItemTypeId = {model.InventoryItemTypeId} ~ Name = {model.Name} ~ ModelId = {(itemType.ModelId.HasValue ? itemType.ModelId.Value : "null")} ~ InventoryItemTypeClassificationId = {model.InventoryItemTypeClassificationId}  ~ BrandId = {model.BrandId} ~ CostCenterId = {model.CostCenterId} ~ InventoryItemTypeUnitId = {model.InventoryItemTypeUnitId} ~ Description = {model.Description}  ~ WarehouseId = {model.WarehouseId}  ~ AutoGenerateSerial = {model.AutoGenerateSerial}]",

            };
            if (model.ItemImage is not null)
            {
                itemType.PathImage = UploadedFile(model.ItemImage, "Images/Inventory/ItemType/");
            }
            itemType.Name = model.Name.Trim();
            itemType.ModelId = model.ModelId;
            itemType.InventoryItemTypeClassificationId = model.InventoryItemTypeClassificationId;
            itemType.BrandId = model.BrandId;          
            itemType.CostCenterId = model.CostCenterId;
            itemType.InventoryItemTypeUnitId = model.InventoryItemTypeUnitId;
           
            itemType.Description = model.Description.Trim();
            if (!hasItems)
            {
                itemType.AutoGenerateSerial = model.AutoGenerateSerial;
                itemType.WarehouseId = model.WarehouseId;
            }
          
            itemType.IsEnabled = true;
            if (model.InventoryItemTypeUnitId == 1)
                itemType.IsQuantitative = true;
            else
                itemType.IsQuantitative = false;           
            unitOfWork.Complete();           
            unitOfWork.InventoryLog.Add(inventoryLog);
            unitOfWork.Complete();
            return itemType.InventoryItemTypeId;
        }

        internal bool CheckIfInventoryItemTypeHasItems(int itemTypeId)
        {
          var item= unitOfWork.InventoryItems.GetOne(c => c.InventoryItemTypeId == itemTypeId);
            if (item is not null)
            {
                return true;
            }
            return false;
        }

        public ItemTypeViewModel PrepareCreatePageInventoryItem(int itemTypeId, ItemTypeViewModel model)
        {
            try
            {
                //ItemTypeViewModel model = new ItemTypeViewModel();
                var ItemType = unitOfWork.InventoryItemType.GetOne(c => c.InventoryItemTypeId == itemTypeId, new[] { "InventoryItemStatusInventoryItemTypes.InventoryItemStatus" });
                if (ItemType != null)
                {
                    model.InventoryItemTypeId = itemTypeId;
                    model.InventoryItemTypeName = ItemType.Name;
                    model.IsQuantitative = ItemType.IsQuantitative;
                    model.AutoGenerateSerial = ItemType.AutoGenerateSerial;
                    model.WarehouseId = ItemType.WarehouseId;
                    model.unitId = ItemType.InventoryItemTypeUnitId.HasValue ? ItemType.InventoryItemTypeUnitId.Value : -1 ;
                    model.Suppliers = new SelectList(unitOfWork.Vendors.GetAll(), "VendorId", "Name");
                    model.CodeType = new SelectList(unitOfWork.CodeTypes.GetAll(), "CodeTypeId", "Name");
                    model.Warehouses = new SelectList(unitOfWork.Warehouse.GetAllWithCriteria(c => c.IsEnabled.Value && c.WarehouseId == ItemType.WarehouseId, new[] { "WarehouseInventoryItemTypeClassifications" }), "WarehouseId", "Name");
                    model.status = new SelectList(ItemType.InventoryItemStatusInventoryItemTypes.Select(c => c.InventoryItemStatus), "InventoryItemStatusId", "Name");
                    return model;
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }          
        }

        internal int DeleteStauts(int id)
        {

            try
            {
                var status = unitOfWork.InventoryItemStatus.GetOne(c => c.InventoryItemStatusId == id);
                if (status is not null)
                {
                    var InventoryItemStatusInventoryItemType = unitOfWork.InventoryItemStatusInventoryItemType.GetAllWithCriteria(c => c.InventoryItemStatusId == id).Count();
                    if (InventoryItemStatusInventoryItemType > 0)
                    {
                        return 0;
                    }
                    var InventoryItemStatusLog = unitOfWork.InventoryItemStatusLog.GetAllWithCriteria(c => c.InventoryItemStatusId == id).Count();
                    if (InventoryItemStatusLog > 0)
                    {
                        return 0;
                    }
                    var InventoryTransactionDetail = unitOfWork.InventoryTransactionDetail.GetAllWithCriteria(c => c.InventoryItemStatusId == id).Count();
                    if (InventoryTransactionDetail > 0)
                    {
                        return 0;
                    }
                    var InventoryItems = unitOfWork.InventoryItems.GetAllWithCriteria(c => c.InventoryItemStatusId == id).Count();
                    if (InventoryItems > 0)
                    {
                        return 0;
                    }
                    if (InventoryItemStatusInventoryItemType <= 0 && InventoryItemStatusLog <= 0 && InventoryTransactionDetail <= 0 && InventoryItems <= 0)
                    {
                        InventoryLog inventoryLog = new InventoryLog()
                        {
                            CreateDT = DateTime.Now,
                            InventoryLogOperationID = 3,
                            InventoryLogTableID = 7,
                            SystemUserID = "1",
                            ObjectID = status.InventoryItemStatusId,
                            Object1 = $"InventoryItemStatusId = {status.InventoryItemStatusId} ~ Name = {status.Name.Trim()}",
                        };
                        unitOfWork.InventoryLog.Add(inventoryLog);
                        unitOfWork.InventoryItemStatus.Delete(status);
                        unitOfWork.Complete();
                    }
                    return 1;
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
         
        }
        internal int DeleteBrand(int id)
        {

            try
            {
                var brand = unitOfWork.Brands.GetOne(c => c.BrandId == id);
                if (brand is not null)
                {
                    var InventoryItemType = unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.BrandId == id).Count();
                    if (InventoryItemType > 0)
                    {
                        return 0;
                    }
                    var model = unitOfWork.Models.GetAllWithCriteria(c => c.BrandId == id).Count();
                    if (model > 0)
                    {
                        return 0;
                    }
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 3,
                        InventoryLogTableID = 10,
                        SystemUserID = "1",
                        ObjectID = brand.BrandId,
                        Object1 = $"BrandId = {brand.BrandId} ~ Name = {brand.Name}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                    unitOfWork.InventoryItemCategoryBrand.DeleteRange(unitOfWork.InventoryItemCategoryBrand.GetAllWithCriteria(c => c.BrandId == brand.BrandId));
                    unitOfWork.Brands.Delete(brand);
                    unitOfWork.Complete();
                    return 1;
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        internal int DeleteModel(int id)
        {

            try
            {
                var model = unitOfWork.Models.GetOne(c => c.ModelId == id);
                if (model is not null)
                {
                    var InventoryItemType = unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.ModelId == id).Count();
                    if (InventoryItemType > 0)
                    {
                        return 0;
                    }


                    unitOfWork.Models.Delete(model);
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 3,
                        InventoryLogTableID = 11,
                        SystemUserID = "1",
                        ObjectID = model.ModelId,
                        Object1 = $"ModelId = {model.ModelId} ~ Name = {model.Name} ~ BrandId = {model.BrandId}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                    return 1;
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        internal InventoryItemTypeIndexPagingViewModel SearchInventoryItemType(int elementID, string label, long warehousesId)
        {
            List<InventoryItemType> itemTypes = new List<InventoryItemType>();
            var itemWithid =unitOfWork.InventoryItemType.GetOne(c => c.IsEnabled && c.InventoryItemTypeId == elementID, new[] { "InventoryItemTypeClassification", "InventoryItemTypeUnit", "Brand", "Model", "Warehouse" });
            if (itemWithid is not null)
            {
                itemTypes.Add(itemWithid);
            }
            if (!string.IsNullOrEmpty(label))
            {
                var s = label.Trim().Split(" ");
                foreach (var item in s)
                {

                    if (itemTypes.Count < TablesMaxRows.InventoryItemTypeIndex)
                    {
                        var itemsWithStartsWith = unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.Name.ToLower().StartsWith(item.ToLower()) && c.IsEnabled && c.InventoryItemTypeId != elementID && (warehousesId > 0 ? c.WarehouseId == warehousesId : true), new[] { "InventoryItemTypeClassification", "InventoryItemTypeUnit", "Brand", "Model", "Warehouse" }).ToList();
                        if (itemsWithStartsWith is not null && itemsWithStartsWith.Count > 0)
                        {
                            //itemTypes.AddRange(itemsWithStartsWith);
                            foreach (var element in itemsWithStartsWith)
                            {
                                var existing = itemTypes.FirstOrDefault(c => c.InventoryItemTypeId == element.InventoryItemTypeId);
                                if (existing is null)
                                {
                                    itemTypes.Add(element);
                                }
                            }
                        }

                        var itemsWithContains = unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.IsEnabled && !c.Name.ToLower().StartsWith(item.ToLower()) && c.Name.ToLower().Contains(label.ToLower()) && c.InventoryItemTypeId != elementID && (warehousesId > 0 ? c.WarehouseId == warehousesId : true), new[] { "InventoryItemTypeClassification", "InventoryItemTypeUnit", "Brand", "Model", "Warehouse" }).ToList();

                        if (itemsWithContains is not null && itemsWithContains.Count > 0)
                        {
                            foreach (var element in itemsWithContains)
                            {
                               var existing = itemTypes.FirstOrDefault(c=>c.InventoryItemTypeId == element.InventoryItemTypeId);
                                if (existing is null )
                                {
                                    itemTypes.Add(element);
                                }
                            }
                           
                        }
                        if (itemTypes.Count > TablesMaxRows.InventoryItemTypeIndex)
                        {
                            itemTypes.RemoveRange(TablesMaxRows.InventoryItemTypeIndex, itemTypes.Count - TablesMaxRows.InventoryItemTypeIndex);
                        }
                    }
                    else
                    {
                        itemTypes.RemoveRange(TablesMaxRows.InventoryItemTypeIndex, itemTypes.Count - TablesMaxRows.InventoryItemTypeIndex);
                    }
                }
            }

          
            if (warehousesId > 0 && string.IsNullOrEmpty(label))
            {
                itemTypes = unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.IsEnabled && c.InventoryItemTypeId != elementID && c.WarehouseId == warehousesId, new[] { "InventoryItemTypeClassification", "InventoryItemTypeUnit", "Brand", "Model", "Warehouse" }).ToList();
                if (itemTypes.Count > TablesMaxRows.InventoryItemTypeIndex)
                {
                    itemTypes.RemoveRange(TablesMaxRows.InventoryItemTypeIndex, itemTypes.Count - TablesMaxRows.InventoryItemTypeIndex);
                }
            }
            InventoryItemTypeIndexPagingViewModel viewModel = new InventoryItemTypeIndexPagingViewModel();

            viewModel.itemTypes = mapper.Map<IEnumerable<InventoryItemTypeIndexViewModel>>(itemTypes).ToList();           
            viewModel.PageCount = 1;
            viewModel.CurrentPageIndex = 1;
            viewModel.itemsCount = itemTypes.Count;
            viewModel.Tablelength = TablesMaxRows.InventoryItemTypeIndex;
            return viewModel;
        }
        internal InventoryTablelPagingViewModel<Model> SearchInventoryModel(int elementID, string label, int BrandId)
        {
            List<Model> itemTypes = new List<Model>();
            var itemWithid = unitOfWork.Models.GetOne(c => c.ModelId == elementID, new[] { "Brand" });
            if (itemWithid is not null)
            {
                itemTypes.Add(itemWithid);
            }
            if (!string.IsNullOrEmpty(label))
            {
                var s = label.Trim().Split(" ");
                foreach (var item in s)
                {

                    if (itemTypes.Count < TablesMaxRows.InventoryModelIndex)
                    {
                        var itemsWithStartsWith = unitOfWork.Models.GetAllWithCriteria(c => c.Name.ToLower().StartsWith(item.ToLower()) && c.ModelId != elementID && (BrandId > 0 ? c.BrandId == BrandId : true), new[] { "Brand" }).ToList();
                        if (itemsWithStartsWith is not null && itemsWithStartsWith.Count > 0)
                        {
                            //itemTypes.AddRange(itemsWithStartsWith);
                            foreach (var element in itemsWithStartsWith)
                            {
                                var existing = itemTypes.FirstOrDefault(c => c.ModelId == element.ModelId);
                                if (existing is null)
                                {
                                    itemTypes.Add(element);
                                }
                            }
                        }

                        var itemsWithContains = unitOfWork.Models.GetAllWithCriteria(c => !c.Name.ToLower().StartsWith(item.ToLower()) && c.Name.ToLower().Contains(label.ToLower()) && c.ModelId != elementID && (BrandId > 0 ? c.BrandId == BrandId : true), new[] { "Brand" }).ToList();

                        if (itemsWithContains is not null && itemsWithContains.Count > 0)
                        {
                            foreach (var element in itemsWithContains)
                            {
                                var existing = itemTypes.FirstOrDefault(c => c.ModelId == element.ModelId);
                                if (existing is null)
                                {
                                    itemTypes.Add(element);
                                }
                            }

                        }
                        if (itemTypes.Count > TablesMaxRows.InventoryModelIndex)
                        {
                            itemTypes.RemoveRange(TablesMaxRows.InventoryModelIndex, itemTypes.Count - TablesMaxRows.InventoryModelIndex);
                        }
                    }
                    else
                    {
                        itemTypes.RemoveRange(TablesMaxRows.InventoryModelIndex, itemTypes.Count - TablesMaxRows.InventoryModelIndex);
                    }
                }
            }


            if (BrandId > 0 && string.IsNullOrEmpty(label))
            {
                itemTypes = unitOfWork.Models.GetAllWithCriteria(c => c.ModelId != elementID && c.BrandId == BrandId, new[] { "Brand" }).ToList();
                if (itemTypes.Count > TablesMaxRows.InventoryModelIndex)
                {
                    itemTypes.RemoveRange(TablesMaxRows.InventoryModelIndex, itemTypes.Count - TablesMaxRows.InventoryModelIndex);
                }
            }
            InventoryTablelPagingViewModel<Model> viewModel = new InventoryTablelPagingViewModel<Model>();

            viewModel.items = itemTypes.ToList();
            viewModel.PageCount = 1;
            viewModel.CurrentPageIndex = 1;
            viewModel.itemsCount = itemTypes.Count;
            viewModel.Tablelength = TablesMaxRows.InventoryModelIndex;
            return viewModel;
        }
        internal InventoryTablelPagingViewModel<Vendor> SearchInventoryVendor(int elementID, string label)
        {
            List<Vendor> vendors = new List<Vendor>();
            var vendorWithid = unitOfWork.Vendors.GetOne(c =>  c.VendorId == elementID);
            if (vendorWithid is not null)
            {
                vendors.Add(vendorWithid);
            }
            if (!string.IsNullOrEmpty(label))
            {
                var s = label.Trim().Split(" ");
                foreach (var item in s)
                {

                    if (vendors.Count < TablesMaxRows.VendorIndex)
                    {
                        var itemsWithStartsWith = unitOfWork.Vendors.GetAllWithCriteria(c => c.Name.ToLower().StartsWith(item.ToLower())  && c.VendorId != elementID).ToList();
                        if (itemsWithStartsWith is not null && itemsWithStartsWith.Count > 0)
                        {
                            //itemTypes.AddRange(itemsWithStartsWith);
                            foreach (var element in itemsWithStartsWith)
                            {
                                var existing = vendors.FirstOrDefault(c => c.VendorId == element.VendorId);
                                if (existing is null)
                                {
                                    vendors.Add(element);
                                }
                            }
                        }

                        var itemsWithContains = unitOfWork.Vendors.GetAllWithCriteria(c => !c.Name.ToLower().StartsWith(item.ToLower()) && c.Name.ToLower().Contains(label.ToLower())).ToList();

                        if (itemsWithContains is not null && itemsWithContains.Count > 0)
                        {
                            foreach (var element in itemsWithContains)
                            {
                                var existing = vendors.FirstOrDefault(c => c.VendorId == element.VendorId);
                                if (existing is null)
                                {
                                    vendors.Add(element);
                                }
                            }

                        }
                        if (vendors.Count > TablesMaxRows.VendorIndex)
                        {
                            vendors.RemoveRange(TablesMaxRows.VendorIndex, vendors.Count - TablesMaxRows.VendorIndex);
                        }
                    }
                    else
                    {
                        vendors.RemoveRange(TablesMaxRows.VendorIndex, vendors.Count - TablesMaxRows.VendorIndex);
                    }
                }
            }



            InventoryTablelPagingViewModel<Vendor> viewModel = new InventoryTablelPagingViewModel<Vendor>();

            viewModel.items = vendors.ToList();
            viewModel.PageCount = 1;
            viewModel.CurrentPageIndex = 1;
            viewModel.itemsCount = vendors.Count;
            viewModel.Tablelength = TablesMaxRows.VendorIndex;
            return viewModel;
        }
        internal InventoryTablelPagingViewModel<Brand> SearchInventoryBrand(int elementID, string label)
        {
            List<Brand> brands = new List<Brand>();
            var brandWithid = unitOfWork.Brands.GetOne(c => c.BrandId == elementID);
            if (brandWithid is not null)
            {
                brands.Add(brandWithid);
            }
            if (!string.IsNullOrEmpty(label))
            {
                var s = label.Trim().Split(" ");
                foreach (var item in s)
                {

                    if (brands.Count < TablesMaxRows.InventoryBrandIndex)
                    {
                        var itemsWithStartsWith = unitOfWork.Brands.GetAllWithCriteria(c => c.Name.ToLower().StartsWith(item.ToLower()) && c.BrandId != elementID).ToList();
                        if (itemsWithStartsWith is not null && itemsWithStartsWith.Count > 0)
                        {
                            //itemTypes.AddRange(itemsWithStartsWith);
                            foreach (var element in itemsWithStartsWith)
                            {
                                var existing = brands.FirstOrDefault(c => c.BrandId == element.BrandId);
                                if (existing is null)
                                {
                                    brands.Add(element);
                                }
                            }
                        }

                        var itemsWithContains = unitOfWork.Brands.GetAllWithCriteria(c => !c.Name.ToLower().StartsWith(item.ToLower()) && c.Name.ToLower().Contains(label.ToLower())).ToList();

                        if (itemsWithContains is not null && itemsWithContains.Count > 0)
                        {
                            foreach (var element in itemsWithContains)
                            {
                                var existing = brands.FirstOrDefault(c => c.BrandId == element.BrandId);
                                if (existing is null)
                                {
                                    brands.Add(element);
                                }
                            }

                        }
                        if (brands.Count > TablesMaxRows.InventoryBrandIndex)
                        {
                            brands.RemoveRange(TablesMaxRows.InventoryBrandIndex, brands.Count - TablesMaxRows.InventoryBrandIndex);
                        }
                    }
                    else
                    {
                        brands.RemoveRange(TablesMaxRows.InventoryBrandIndex, brands.Count - TablesMaxRows.InventoryBrandIndex);
                    }
                }
            }



            InventoryTablelPagingViewModel<Brand> viewModel = new InventoryTablelPagingViewModel<Brand>();

            viewModel.items = brands.ToList();
            viewModel.PageCount = 1;
            viewModel.CurrentPageIndex = 1;
            viewModel.itemsCount = brands.Count;
            viewModel.Tablelength = TablesMaxRows.InventoryBrandIndex;
            return viewModel;
        }
        internal InventoryTablelPagingViewModel<InventoryItemTypeClassification> SearchInventoryClassification(int elementID, string label)
        {
            List<InventoryItemTypeClassification> classifications = new List<InventoryItemTypeClassification>();
            var classificationWithid = unitOfWork.InventoryItemTypeClassifications.GetOne(c => c.InventoryItemTypeClassificationId == elementID);
            if (classificationWithid is not null)
            {
                classifications.Add(classificationWithid);
            }
            if (!string.IsNullOrEmpty(label))
            {
                var s = label.Trim().Split(" ");
                foreach (var item in s)
                {

                    if (classifications.Count < TablesMaxRows.InventoryClassificationIndex)
                    {
                        var itemsWithStartsWith = unitOfWork.InventoryItemTypeClassifications.GetAllWithCriteria(c => c.Name.ToLower().StartsWith(item.ToLower()) && c.InventoryItemTypeClassificationId != elementID).ToList();
                        if (itemsWithStartsWith is not null && itemsWithStartsWith.Count > 0)
                        {
                            //itemTypes.AddRange(itemsWithStartsWith);
                            foreach (var element in itemsWithStartsWith)
                            {
                                var existing = classifications.FirstOrDefault(c => c.InventoryItemTypeClassificationId == element.InventoryItemTypeClassificationId);
                                if (existing is null)
                                {
                                    classifications.Add(element);
                                }
                            }
                        }

                        var itemsWithContains = unitOfWork.InventoryItemTypeClassifications.GetAllWithCriteria(c => !c.Name.ToLower().StartsWith(item.ToLower()) && c.Name.ToLower().Contains(label.ToLower())).ToList();

                        if (itemsWithContains is not null && itemsWithContains.Count > 0)
                        {
                            foreach (var element in itemsWithContains)
                            {
                                var existing = classifications.FirstOrDefault(c => c.InventoryItemTypeClassificationId == element.InventoryItemTypeClassificationId);
                                if (existing is null)
                                {
                                    classifications.Add(element);
                                }
                            }

                        }
                        if (classifications.Count > TablesMaxRows.InventoryClassificationIndex)
                        {
                            classifications.RemoveRange(TablesMaxRows.InventoryClassificationIndex, classifications.Count - TablesMaxRows.InventoryClassificationIndex);
                        }
                    }
                    else
                    {
                        classifications.RemoveRange(TablesMaxRows.InventoryClassificationIndex, classifications.Count - TablesMaxRows.InventoryClassificationIndex);
                    }
                }
            }



            InventoryTablelPagingViewModel<InventoryItemTypeClassification> viewModel = new InventoryTablelPagingViewModel<InventoryItemTypeClassification>();

            viewModel.items = classifications.ToList();
            viewModel.PageCount = 1;
            viewModel.CurrentPageIndex = 1;
            viewModel.itemsCount = classifications.Count;
            viewModel.Tablelength = TablesMaxRows.InventoryClassificationIndex;
            return viewModel;
        }
        internal int DeleteRelationInventoryItemTypeAndStatus(int statusId, int itemTypeId)
        {
            try
            {
                var InventoryItemTypeStatus = unitOfWork.InventoryItemStatusInventoryItemType.GetOne(c => c.InventoryItemTypeId == itemTypeId && c.InventoryItemStatusId == statusId);
                if (InventoryItemTypeStatus is not null)
                {
                    var InventoryItemType = unitOfWork.InventoryItems.GetAllWithCriteria(c => c.InventoryItemStatusId == statusId && c.InventoryItemTypeId == itemTypeId).Count();
                    if (InventoryItemType > 0)
                    {
                        return 0;
                    }
                    //var InventoryItemStatusLog = unitOfWork.InventoryItemStatusLog.GetAllWithCriteria(c => c.InventoryItemStatusId == statusId && c.InventoryItemId == itemTypeId).Count();
                    //if (InventoryItemStatusLog > 0)
                    //{
                    //    return 0;
                    //}
                    //
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 3,
                        InventoryLogTableID = 8,
                        SystemUserID = "1",
                        ObjectID = InventoryItemTypeStatus.InventoryItemStatusId,
                        Object1 = $"InventoryItemStatusId = {InventoryItemTypeStatus.InventoryItemStatusId} ~ InventoryItemTypeId = {InventoryItemTypeStatus.InventoryItemTypeId}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);                    
                    unitOfWork.InventoryItemStatusInventoryItemType.Delete(InventoryItemTypeStatus);
                    unitOfWork.Complete();
                    return 1;
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        internal List<AutoCompleteViewModel> AutoCompleteInventoryItemType(string prefix)
        {
            List<AutoCompleteViewModel> values = 
                unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.IsEnabled && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c=> new AutoCompleteViewModel {val=c.InventoryItemTypeId , label=c.Name}).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.IsEnabled && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.InventoryItemTypeId, label = c.Name }).ToList());
                if (values.Count >15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal List<AutoCompleteViewModel> AutoCompleteInventoryModel(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.Models.GetAllWithCriteria(c => c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.ModelId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.Models.GetAllWithCriteria(c => !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.ModelId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }
        internal List<AutoCompleteViewModel> AutoCompleteInventoryVendor(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.Vendors.GetAllWithCriteria(c =>  c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VendorId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.Vendors.GetAllWithCriteria(c => !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VendorId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }
        internal List<AutoCompleteViewModel> AutoCompleteInventoryBrand(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.Brands.GetAllWithCriteria(c => c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.BrandId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.Brands.GetAllWithCriteria(c => !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.BrandId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }
        internal List<AutoCompleteViewModel> AutoCompleteInventoryClassification(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.InventoryItemTypeClassifications.GetAllWithCriteria(c => c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.InventoryItemTypeClassificationId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.InventoryItemTypeClassifications.GetAllWithCriteria(c => !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.InventoryItemTypeClassificationId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal int DeleteUnit(int id)
        {

            try
            {
                var unit = unitOfWork.InventoryItemTypeUnits.GetOne(c => c.InventoryItemTypeUnitId == id);
                if (unit is not null)
                {
                    var InventoryItemType = unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.InventoryItemTypeUnitId == id).Count();
                    if (InventoryItemType > 0)
                    {
                        return 0;
                    }

                   
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 3,
                        InventoryLogTableID = 9,
                        SystemUserID = "1",
                        ObjectID = unit.InventoryItemTypeUnitId,
                        Object1 = $"InventoryItemTypeUnitId = {unit.InventoryItemTypeUnitId} ~ Name = {unit.Name.Trim()} ~ Description = {unit.Description.Trim()}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                
                    unitOfWork.InventoryItemTypeUnits.Delete(unit);
                    unitOfWork.Complete();
                    return 1;
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        internal int DeleteClassification(int id)
        {

            try
            {
                var classification = unitOfWork.InventoryItemTypeClassifications.GetOne(c => c.InventoryItemTypeClassificationId == id);
                if (classification is not null)
                {
                    var InventoryItemType = unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.InventoryItemTypeClassificationId == id).Count();
                    if (InventoryItemType > 0)
                    {
                        return 0;
                    }
                    var Warehouseclassifications = unitOfWork.WarehouseInventoryItemTypeClassification.GetAllWithCriteria(c => c.InventoryItemTypeClassificationId == id);
                    if (Warehouseclassifications is not null && Warehouseclassifications.Count()>0)
                    {
                        foreach (var item in Warehouseclassifications)
                        {
                            InventoryLog inventoryLog = new InventoryLog()
                            {
                                CreateDT = DateTime.Now,
                                InventoryLogOperationID = 3,
                                InventoryLogTableID = 5,
                                SystemUserID = "1",
                                ObjectID = item.InventoryItemTypeClassificationId,
                                Object1 = $"InventoryItemTypeClassificationId = {item.InventoryItemTypeClassificationId} ~ WarehouseId = {item.WarehouseId}",
                            };
                            unitOfWork.InventoryLog.Add(inventoryLog);
                        }
                      
                    }
                    unitOfWork.WarehouseInventoryItemTypeClassification.DeleteRange(Warehouseclassifications);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog2 = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 3,
                        InventoryLogTableID = 4,
                        SystemUserID = "1",
                        ObjectID = classification.InventoryItemTypeClassificationId,
                        Object1 = $"InventoryItemTypeClassificationId = {classification.InventoryItemTypeClassificationId} ~ Name = {classification.Name}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog2);
                    unitOfWork.Complete();
                    unitOfWork.InventoryItemTypeClassifications.Delete(classification);
                    unitOfWork.Complete();
                    return 1;
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        internal int DeleteVendor(int id)
        {

            try
            {
                var vendor = unitOfWork.Vendors.GetOne(c => c.VendorId == id);
                if (vendor is not null)
                {
                    var InventoryItemType = unitOfWork.InventoryItems.GetAllWithCriteria(c => c.VendorId == id).Count();
                    if (InventoryItemType > 0)
                    {
                        return 0;
                    }

                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 3,
                        InventoryLogTableID = 13,
                        SystemUserID = "1",
                        ObjectID = vendor.VendorId,
                        Object1 = $"VendorId = {vendor.VendorId} ~ Name = {vendor.Name} ~ CreateDts = {vendor.CreateDts} ",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Vendors.Delete(vendor);
                    unitOfWork.Complete();
                    return 1;
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
       
        internal int AddInventoryItem(ItemTypeViewModel model)
        {              
                if (model.unitId == 1)
                {
                    if (!model.AutoGenerateSerial)
                    {
                        string[] SerialNumbers = model.SerialNumbers.Split('+');
                        foreach (var item in SerialNumbers)
                        {
                            addItem(model, item, 1);                          
                        }
                    }
                    else
                    {
                        for (int i = 0; i < model.Quantity; i++)
                        {
                            addItem( model, "Auto~" + Guid.NewGuid().ToString(), 1);                          
                        }
                    }
                }
                else
                {
                    addItem(model, "Auto~" + Guid.NewGuid().ToString(), model.Quantity);                  
                }
                return 1;   
          
        }
       private void addItem(ItemTypeViewModel model,string serialNumber,decimal Quantity)
        {

            InventoryItem inventoryItem = new InventoryItem()
            {
                CreateDts = model.CreateDT,
                CurrentQuantity = Quantity,
                Quantity = Quantity,
                InventoryItemStatusId = model.InventoryItemStatusId ,
                VendorId = model.VendorId,
                UnitCost = model.UnitCost,
                SystemUserId = 1,
                SerialNumber = serialNumber,
                InventoryLocationId = model.InventoryLocationId > 0 ? model.InventoryLocationId : null,
                Notes = model.Notes,
                IssueNumber = model.IssueNumber,
                InventoryItemTypeId = model.InventoryItemTypeId,
                CodeTypeId = model.CodeTypeId.HasValue && model.CodeTypeId.Value > 0 && !string.IsNullOrEmpty(model.Code) ? model.CodeTypeId.Value : null,
                Code = model.CodeTypeId.HasValue && model.CodeTypeId.Value > 0 && !string.IsNullOrEmpty(model.Code) ? model.Code : null ,
                LocationId = GetLocationIDByWarehouseID(model.WarehouseId).Value,

            };
            unitOfWork.InventoryItems.Add(inventoryItem);
            unitOfWork.Complete();
            DateTime now = DateTime.Now;
            InventoryLog inventoryLog = new InventoryLog()
            {
                CreateDT = now,
                InventoryLogOperationID = 1,
                InventoryLogTableID = 12,
                SystemUserID = "1",
                ObjectID = inventoryItem.InventoryItemId,
                Object1 = $"SerialNumber = {inventoryItem.SerialNumber} ~ UnitCost = {inventoryItem.UnitCost} ~ VendorId = {inventoryItem.VendorId} ~ InventoryItemStatusId = {inventoryItem.InventoryItemStatusId} ~ CreateDts = {inventoryItem.CreateDts} ~ CurrentQuantity = {inventoryItem.CurrentQuantity} ~ Quantity = {inventoryItem.Quantity}",
            };
            unitOfWork.InventoryLog.Add(inventoryLog);
            unitOfWork.Complete();
            if (model.WarehouseId == 1)
            {
                ParameterValue parameterValue = new ParameterValue()
                {
                    ObjectId = inventoryItem.InventoryItemId,
                    Value = model.PartNumber,
                    ParameterId = 300
                };
                unitOfWork.ParameterValue.Add(parameterValue);
                unitOfWork.Complete();
                InventoryLog inventoryLogPartNumber = new InventoryLog()
                {
                    CreateDT = now,
                    InventoryLogOperationID = 1,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = parameterValue.ParameterValueId,
                    Object1 = $"ObjectId = {inventoryItem.InventoryItemId} ~ Value = {model.PartNumber} ~ ParameterId =300 ",
                    Description = "PartNumber"
                };
                unitOfWork.InventoryLog.Add(inventoryLogPartNumber);
                unitOfWork.Complete();
            }
            else if (model.WarehouseId == 2)
            {
                ParameterValue Threshold=null;
                if (model.Threshold.HasValue)
                {
                     Threshold = new ParameterValue()
                    {
                        ObjectId = inventoryItem.InventoryItemId,
                        Value = model.Threshold.ToString(),
                        ParameterId = 203
                    };
                    unitOfWork.ParameterValue.Add(Threshold);
                }
                      
             
                ParameterValue TireSize = new ParameterValue()
                {
                    ObjectId = inventoryItem.InventoryItemId,
                    Value = model.TireSize.ToString(),
                    ParameterId = 202
                };
                unitOfWork.ParameterValue.Add(TireSize);
               
                ParameterValue TirePattern = new ParameterValue()
                {
                    ObjectId = inventoryItem.InventoryItemId,
                    Value = model.TirePattern.ToString(),
                    ParameterId = 201
                };
                unitOfWork.ParameterValue.Add(TirePattern);
              
                ParameterValue StandardTreadDepth = new ParameterValue()
                {
                    ObjectId = inventoryItem.InventoryItemId,
                    Value = model.StandardTreadDepth.Value.ToString(),
                    ParameterId = 200
                };
                unitOfWork.ParameterValue.Add(StandardTreadDepth);
               
                unitOfWork.Complete();

                if (model.Threshold.HasValue &&  Threshold is not null)
                {
                    InventoryLog inventoryLogThreshold = new InventoryLog()
                    {
                        CreateDT = now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 16,
                        SystemUserID = "1",
                        ObjectID = Threshold.ParameterValueId,
                        Object1 = $"ObjectId = {Threshold.ObjectId} ~ Value = {Threshold.Value} ~ ParameterId =203 ",
                        Description = "Threshold"
                    };
                    unitOfWork.InventoryLog.Add(inventoryLogThreshold);
                }
               
                InventoryLog inventoryLogTireSize = new InventoryLog()
                {
                    CreateDT = now,
                    InventoryLogOperationID = 1,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = TireSize.ParameterValueId,
                    Object1 = $"ObjectId = {TireSize.ObjectId} ~ Value = {TireSize.Value} ~ ParameterId =202 ",
                    Description = "TireSize"
                };
                unitOfWork.InventoryLog.Add(inventoryLogTireSize);
                InventoryLog inventoryLogTirePattern = new InventoryLog()
                {
                    CreateDT = now,
                    InventoryLogOperationID = 1,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = TirePattern.ParameterValueId,
                    Object1 = $"ObjectId = {TirePattern.ObjectId} ~ Value = {TirePattern.Value} ~ ParameterId =201 ",
                    Description = "TirePattern"
                };
                unitOfWork.InventoryLog.Add(inventoryLogTirePattern);
                InventoryLog inventoryLogStandardTreadDepth = new InventoryLog()
                {
                    CreateDT = now,
                    InventoryLogOperationID = 1,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = StandardTreadDepth.ParameterValueId,
                    Object1 = $"ObjectId = {StandardTreadDepth.ObjectId} ~ Value = {StandardTreadDepth.Value} ~ ParameterId =200 ",
                    Description = "StandardTreadDepth"
                };
                unitOfWork.InventoryLog.Add(inventoryLogStandardTreadDepth);
                unitOfWork.Complete();
            }
            else if (model.WarehouseId == 3)
            {
                ParameterValue parameterValue = new ParameterValue()
                {
                    ObjectId = inventoryItem.InventoryItemId,
                    Value = model.Viscosityid.ToString(),
                    ParameterId = 100
                };
                unitOfWork.ParameterValue.Add(parameterValue);
                unitOfWork.Complete();
                InventoryLog inventoryLogViscosityid = new InventoryLog()
                {
                    CreateDT = now,
                    InventoryLogOperationID = 1,
                    InventoryLogTableID = 16,
                    SystemUserID = "1",
                    ObjectID = parameterValue.ParameterValueId,
                    Object1 = $"ObjectId = {parameterValue.ObjectId} ~ Value = {parameterValue.Value} ~ ParameterId =100 ",
                    Description = "Viscosityid"
                };
                unitOfWork.InventoryLog.Add(inventoryLogViscosityid);
                unitOfWork.Complete();
            }

            InventoryItemStatusLog statusLog = new InventoryItemStatusLog
            {
                InventoryItem = inventoryItem,
                InventoryItemStatusId = inventoryItem.InventoryItemStatusId,
                StatusDts = inventoryItem.CreateDts
            };
            unitOfWork.InventoryItemStatusLog.Add(statusLog);
            unitOfWork.Complete();
        }
        private long? GetLocationIDByWarehouseID(long warehouseID)
        {
            return (from w in unitOfWork.Warehouse.GetAll() //.Warehouse
                    join l in unitOfWork.Location.GetAll() /*inventoryContext.Location*/ on w.WarehouseId equals l.LocationObjectId
                    where l.LocationTypeId == 2 && w.WarehouseId == warehouseID
                    select l.LocationId
                ).FirstOrDefault();
        }
        public ViewItemTypeViewModel getInventoryItemTypeById(int id)
        {
            InventoryItemType ItemType = unitOfWork.InventoryItemType.GetOne(c=>c.InventoryItemTypeId==id,new[] { "Brand" , "Model", "Warehouse", "Inventoryitemcategory" , "InventoryItemTypeUnit", "InventoryItemTypeClassification", "InventoryItems", "InventoryItemStatusInventoryItemTypes.InventoryItemStatus" });
            ViewItemTypeViewModel model = null;
            if (ItemType != null)
            {
                model = mapper.Map<ViewItemTypeViewModel>(ItemType);
                if (ItemType.CostCenterId.HasValue)
                {
                    model.CostCenter = unitOfWork.CostCenters.GetOne(c=>c.CostCenterId == model.CostCenterId).Value;
                }
                model.CurrentQuantity = unitOfWork.InventoryItems.GetAllWithCriteria(c => c.InventoryItemTypeId == ItemType.InventoryItemTypeId).Sum(c => c.CurrentQuantity);
                var items = unitOfWork.InventoryItems.GetAllWithCriteria(c => c.InventoryItemTypeId == id /*&& c.Location.LocationTypeId == 2*/, new[] { "Vendor", "Location", "InventoryItemStatus", "InventoryLocation.ParentInventoryLocation", "InventoryLocation.ParentInventoryLocation.ParentInventoryLocation", "InventoryLocation.Warehouse", "CodeType" , "InventoryItemType.Warehouse" }).OrderByDescending(c=>c.CreateDts);
                model.Items = mapper.Map<List<ViewItemViewModel>>(items);
                model.Status = ItemType.InventoryItemStatusInventoryItemTypes.Select(c => c.InventoryItemStatus).OrderBy(c=>c.InventoryItemStatusId).ToList();
              
            }

            return model;
        }
        public SelectList GetWarehouses()
        {
            var Warehouses = unitOfWork.Warehouse.GetAllWithCriteria(c => c.IsEnabled.Value);
            if (Warehouses.Count() > 0)
            {
                return new SelectList(Warehouses, "WarehouseId", "Name");
            }
            return null;
        }
        public SelectList GetWarehousesForCreateInvItem(long warehouseId)
        {
            var Warehouses = unitOfWork.Warehouse.GetAllWithCriteria(c => c.IsEnabled.Value && c.WarehouseId == warehouseId, new[] { "WarehouseInventoryItemTypeClassifications" });
            if (Warehouses.Count() > 0)
            {
                return new SelectList(Warehouses, "WarehouseId", "Name");
            }
            return null;
        }
        public SelectList GetWarehouses(Warehouse SelectedValues)
        {
            var Warehouses = unitOfWork.Warehouse.GetAllWithCriteria(c => c.IsEnabled.Value);
            if (Warehouses.Count() > 0)
            {
                return new SelectList(Warehouses, "WarehouseId", "Name", SelectedValues);
            }
            return null;
        }
        public List<Warehouse> GetListWarehouses()
        {
           return unitOfWork.Warehouse.GetAllWithCriteria(c => c.IsEnabled.Value).ToList();
          
        }
        public List<InventoryItemIndexViewModel> getAllInventoryItem()
        {            
            var InventoryItems=unitOfWork.InventoryItems.GetAllAsync(new[] { "InventoryItemType", "InventoryItemStatus" , "Brand" , "Vendor" });
            return  mapper.Map<IEnumerable<InventoryItemIndexViewModel>>(InventoryItems.Result).ToList();
        }

        public SelectList GetClassifications(long warehouseId)
        {
            var ClassificationIds = unitOfWork.WarehouseInventoryItemTypeClassification.GetAllWithCriteria(c => c.WarehouseId == warehouseId).Select(c => c.InventoryItemTypeClassificationId).ToList();
            var Classifications = unitOfWork.InventoryItemTypeClassifications.GetAllWithCriteria(c => ClassificationIds.Any(b => b == c.InventoryItemTypeClassificationId));

            if (Classifications.Count() > 0)
            {
                var Classifications2 = Classifications.OrderBy(c => c.Name);
                return new SelectList(Classifications2, "InventoryItemTypeClassificationId", "Name");
            }
            return null;
        }

        internal int AddClassificationToWarehouse(long warehouseId, int classificationId)
        {
            try
            {
                WarehouseInventoryItemTypeClassification inventoryItemTypeClassification = new WarehouseInventoryItemTypeClassification() { WarehouseId = warehouseId, InventoryItemTypeClassificationId = classificationId };
                unitOfWork.WarehouseInventoryItemTypeClassification.Add(inventoryItemTypeClassification);
                InventoryLog inventoryLog = new InventoryLog()
                {
                    CreateDT = DateTime.Now,
                    InventoryLogOperationID = 1,
                    InventoryLogTableID = 5,
                    SystemUserID = "1",
                    ObjectID = inventoryItemTypeClassification.InventoryItemTypeClassificationId,
                    Object1 = $"InventoryItemTypeClassificationId = {inventoryItemTypeClassification.InventoryItemTypeClassificationId} ~ WarehouseId = {inventoryItemTypeClassification.WarehouseId}",
                };
                unitOfWork.InventoryLog.Add(inventoryLog);
                unitOfWork.Complete();
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
           
        }
        internal int AddBrand( string brandName)
        {
            try
            {
                Brand brand = new Brand() { Name= brandName.Trim() };
                 var check=  unitOfWork.Brands.GetOne(c=>c.Name.Trim().ToLower() == brandName.Trim().ToLower());
                if (check == null)
                {
                    unitOfWork.Brands.Add(brand);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 10,
                        SystemUserID = "1",
                        ObjectID = brand.BrandId,
                        Object1 = $"BrandId = {brand.BrandId} ~ Name = {brand.Name}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return 2;
                }
               
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        internal int AddStatus(string statusName)
        {
            try
            {
                InventoryItemStatus status = new InventoryItemStatus() { Name = statusName.Trim() };
                var check = unitOfWork.InventoryItemStatus.GetOne(c => c.Name.Trim().ToLower() == statusName.Trim().ToLower());
                if (check == null)
                {
                    unitOfWork.InventoryItemStatus.Add(status);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 7,
                        SystemUserID = "1",
                        ObjectID = status.InventoryItemStatusId,
                        Object1 = $"InventoryItemStatusId = {status.InventoryItemStatusId} ~ Name = {status.Name.Trim()}",                
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return -2;
                }

                return status.InventoryItemStatusId;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        internal int AddBrand(Brand brand)
        {
            try
            {
                Brand _brand = new Brand() { Name = brand.Name.Trim() };
                var check = unitOfWork.Brands.GetOne(c => c.Name.Trim().ToLower() == _brand.Name.Trim().ToLower());
                if (check == null)
                {
                    unitOfWork.Brands.Add(_brand);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 10,
                        SystemUserID = "1",
                        ObjectID = _brand.BrandId,
                        Object1 = $"BrandId = {_brand.BrandId} ~ Name = {_brand.Name}",                
                    };                   
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return -1;
                }

                return _brand.BrandId;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        internal int AddModel(Model model)
        {
            try
            {
                Model _model = new Model() { Name = model.Name.Trim(),BrandId = model.BrandId };
                var check = unitOfWork.Models.GetOne(c => c.Name.Trim().ToLower() == _model.Name.Trim().ToLower() && c.BrandId == _model.BrandId);
                if (check == null)
                {
                    unitOfWork.Models.Add(_model);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 11,
                        SystemUserID = "1",
                        ObjectID = _model.ModelId,
                        Object1 = $"ModelId = {_model.ModelId} ~ Name = {_model.Name} ~ BrandId = {_model.BrandId}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return -1;
                }

                return _model.ModelId;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        internal int AddUnit(InventoryItemTypeUnit unit)
        {
            try
            {
                InventoryItemTypeUnit _unit = new InventoryItemTypeUnit() { Name = unit.Name.Trim() , Description = unit.Description.Trim()};
                var check = unitOfWork.InventoryItemTypeUnits.GetOne(c => c.Name.Trim().ToLower() == _unit.Name.Trim().ToLower());
                if (check == null)
                {
                    unitOfWork.InventoryItemTypeUnits.Add(_unit);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 9,
                        SystemUserID = "1",
                        ObjectID = _unit.InventoryItemTypeUnitId,
                        Object1 = $"InventoryItemTypeUnitId = {_unit.InventoryItemTypeUnitId} ~ Name = {_unit.Name.Trim()} ~ Description = {_unit.Description.Trim()}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return -1;
                }

                return _unit.InventoryItemTypeUnitId;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        internal int AddClassification(InventoryItemTypeClassification classification ,long WarehouseId)
        {
            try
            {
                InventoryItemTypeClassification _classification = new InventoryItemTypeClassification() { Name = classification.Name.Trim()};
                var check = unitOfWork.InventoryItemTypeClassifications.GetOne(c => c.Name.Trim().ToLower() == _classification.Name.Trim().ToLower());
                if (check == null)
                {
                    
                    unitOfWork.InventoryItemTypeClassifications.Add(_classification);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 4,
                        SystemUserID = "1",
                        ObjectID = _classification.InventoryItemTypeClassificationId,
                        Object1 = $"InventoryItemTypeClassificationId = {_classification.InventoryItemTypeClassificationId} ~ Name = {_classification.Name}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    WarehouseInventoryItemTypeClassification warehouseInventoryItem =   new WarehouseInventoryItemTypeClassification { WarehouseId = WarehouseId, InventoryItemTypeClassificationId = _classification.InventoryItemTypeClassificationId };
                    unitOfWork.WarehouseInventoryItemTypeClassification.Add(warehouseInventoryItem);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog2 = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 5,
                        SystemUserID = "1",
                        ObjectID = warehouseInventoryItem.InventoryItemTypeClassificationId,
                        Object1 = $"InventoryItemTypeClassificationId = {warehouseInventoryItem.InventoryItemTypeClassificationId} ~ WarehouseId = {warehouseInventoryItem.WarehouseId}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog2);
                    unitOfWork.Complete();
                }
                else
                {
                    return -1;
                }

                return _classification.InventoryItemTypeClassificationId;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        internal int AddVendor(Vendor vendor)
        {
            try
            {
                Vendor _vendor = new Vendor() { Name = vendor.Name.Trim(), CreateDts = DateTime.Now };
                var check = unitOfWork.Vendors.GetOne(c => c.Name.Trim().ToLower() == _vendor.Name.Trim().ToLower());
                if (check == null)
                {
                    unitOfWork.Vendors.Add(_vendor);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 13,
                        SystemUserID = "1",
                        ObjectID = _vendor.VendorId,
                        Object1 = $"VendorId = {_vendor.VendorId} ~ Name = {_vendor.Name} ~ CreateDts = {_vendor.CreateDts} ",                     
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return -1;
                }

                return _vendor.VendorId;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        internal void AssignStatusToItemType(int statusId, int itemTypeid)
        {
            InventoryItemStatusInventoryItemType statusInventoryItemType =   new InventoryItemStatusInventoryItemType()
            { InventoryItemStatusId = statusId, InventoryItemTypeId = itemTypeid };
            unitOfWork.InventoryItemStatusInventoryItemType.Add(statusInventoryItemType);
            unitOfWork.Complete();
            InventoryLog inventoryLog = new InventoryLog()
            {
                CreateDT = DateTime.Now,
                InventoryLogOperationID = 1,
                InventoryLogTableID = 8,
                SystemUserID = "1",
                ObjectID = statusInventoryItemType.InventoryItemTypeId,
                Object1 = $"InventoryItemTypeId = {statusInventoryItemType.InventoryItemTypeId} ~ InventoryItemStatusId = {statusInventoryItemType.InventoryItemStatusId}",
            };
            unitOfWork.InventoryLog.Add(inventoryLog);
            unitOfWork.Complete();
        }

        public SelectList GetClassificationsNotContainWarehouseId(long warehouseId)
        {
            var ClassificationIds = unitOfWork.WarehouseInventoryItemTypeClassification.GetAllWithCriteria(c => c.WarehouseId == warehouseId).Select(c => c.InventoryItemTypeClassificationId).ToList();
            var Classifications = unitOfWork.InventoryItemTypeClassifications.GetAllWithCriteria(c => !ClassificationIds.Any(b => b == c.InventoryItemTypeClassificationId));

            if (Classifications.Count() > 0)
            {
               var Classifications2 = Classifications.OrderBy(c => c.Name);
                return new SelectList(Classifications2, "InventoryItemTypeClassificationId", "Name");
            }
            return null;
        }

        internal int AddModelByBrandId(int brandId, string modelName)
        {
            try
            {
               
                var check = unitOfWork.Models.GetOne(c =>  c.Name.Trim().ToLower() == modelName.Trim().ToLower() && c.BrandId == brandId);
                if (check == null)
                {
                    Model model = new Model() { Name = modelName.Trim() ,BrandId= brandId };
                    unitOfWork.Models.Add(model);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 11,
                        SystemUserID = "1",
                        ObjectID = model.BrandId,
                        Object1 = $"ModelId = {model.ModelId} ~ Name = {model.Name} ~ BrandId = {model.BrandId}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return 2;
                }

                return 1;
            }
            catch (Exception)
            {

                return 0;
            }

        }

        internal int AddUnit(string UnitName, string UnitDescription)
        {
            try
            {

                var check = unitOfWork.InventoryItemTypeUnits.GetOne(c => c.Name.Trim().ToLower() == UnitName.Trim().ToLower() || c.Description.Trim().ToLower() == UnitDescription.Trim().ToLower());
                if (check == null)
                {
                    InventoryItemTypeUnit unit = new InventoryItemTypeUnit() { Name = UnitName.Trim(), Description = UnitDescription.Trim() };
                    unitOfWork.InventoryItemTypeUnits.Add(unit);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 9,
                        SystemUserID = "1",
                        ObjectID = unit.InventoryItemTypeUnitId,
                        Object1 = $"InventoryItemTypeUnitId = {unit.InventoryItemTypeUnitId} ~ Name = {unit.Name} ~ Description = {unit.Description}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return 2;
                }

                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        internal int AddClassification(string classificationName)
        {
            try
            {
                var check = unitOfWork.InventoryItemTypeClassifications.GetOne(c => c.Name.Trim().ToLower() == classificationName.Trim().ToLower());
                if (check == null)
                {
                    InventoryItemTypeClassification classification = new InventoryItemTypeClassification() { Name = classificationName.Trim()};
                    unitOfWork.InventoryItemTypeClassifications.Add(classification);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 4,
                        SystemUserID = "1",
                        ObjectID = classification.InventoryItemTypeClassificationId,
                        Object1 = $"InventoryItemTypeClassificationId = {classification.InventoryItemTypeClassificationId} ~ Name = {classification.Name}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return 2;
                }

                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        internal int AddCostCenter(string costCenterName, string CostCenterValue)
        {
            try
            {
                var check = unitOfWork.CostCenters.GetOne(c => c.Enable && c.Name.Trim().ToLower() == costCenterName.Trim().ToLower() );
                if (check == null)
                {
                    CostCenter costCenter = new CostCenter() { Name = costCenterName.Trim(), Value = CostCenterValue.Trim() ,Enable=true,CreateDts=DateTime.Now,Systemusercrate="1" };
                    unitOfWork.CostCenters.Add(costCenter);
                    unitOfWork.Complete();
                    InventoryLog inventoryLog = new InventoryLog()
                    {
                        CreateDT = DateTime.Now,
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 17,
                        SystemUserID = "1",
                        ObjectID = costCenter.CostCenterId,
                        Object1 = $"CostCenterId = {costCenter.CostCenterId} ~ Name = {costCenter.Name} ~ Value = {costCenter.Value}",
                    };
                    unitOfWork.InventoryLog.Add(inventoryLog);
                    unitOfWork.Complete();
                }
                else
                {
                    return 2;
                }

                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public int AddInventoryItemType(CreateInventoryItemTypeViewModel model)
        {
            var itemType = mapper.Map<InventoryItemType>(model);
            itemType.CreateDts = DateTime.Now;
            itemType.SystemUserId = 1;
            //itemType.PathImage = UploadImage(model.ItemImage, "wwwroot/Images/Inventory/ItemType/");
            itemType.PathImage = UploadedFile(model.ItemImage, "Images/Inventory/ItemType/");
            itemType.IsEnabled = true;

            if (model.InventoryItemTypeUnitId == 1)
                itemType.IsQuantitative = true;          
            else
                itemType.IsQuantitative = false;
           
            unitOfWork.InventoryItemType.Add(itemType);
            unitOfWork.Complete();

            InventoryLog inventoryLog = new InventoryLog()
            {
                CreateDT = DateTime.Now,
                InventoryLogOperationID = 1,
                InventoryLogTableID = 6,
                SystemUserID = "1",
                ObjectID = itemType.InventoryItemTypeId,
                Object1 = $"InventoryItemTypeId = {itemType.InventoryItemTypeId} ~ Name = {itemType.Name}",
            };
            unitOfWork.InventoryLog.Add(inventoryLog);
            unitOfWork.Complete();
            return itemType.InventoryItemTypeId;
        }
        private string UploadedFile(IFormFile file,string path)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, path);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
               
            }
            return uniqueFileName;
        }
        //public string UploadImage(IFormFile file,string path)
        //{
        //    //var supportedTypes = new[] { "jpg", "jpeg", "gif", "png", "jfif", "xlsx", "iff", "pjp", "svg", "bmp", "svgz", "webp", "ico", "xbm", "dib", "tif", "pjpeg", "avif" };
        //    string wwwRootPath = path;
        //    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
        //    string extension = Path.GetExtension(file.FileName);          
        //    string pathImage = Path.Combine(wwwRootPath , fileName);
        //    using (var fileStream = new FileStream(pathImage, FileMode.Create))
        //    {
        //        file.CopyTo(fileStream);
        //    }
        //    return pathImage;
        //}
        public SelectList GetUnits()
        {
            var Units = unitOfWork.InventoryItemTypeUnits.GetAll();

            if (Units.Count() > 0)
            {
                var Units2 = Units.OrderBy(c => c.Name);
                return new SelectList(Units2, "InventoryItemTypeUnitId", "Name");
            }
            return null;
        }
        public SelectList CostCenters()
        {
            var costCenters = unitOfWork.CostCenters.GetAll();

            if (costCenters.Count() > 0)
            {
                var costCenters2 = costCenters.OrderBy(c => c.Name);
                return new SelectList(costCenters2, "CostCenterId", "Name");
            }
            return null;
        }
        public SelectList getModelsByBrandID(long brandId)
        {
            var models = unitOfWork.Models.GetAllWithCriteria(c => c.BrandId == brandId);
            if (models.Count() > 0)
            {
               var models2 = models.OrderBy(c => c.Name);
                return new SelectList(models2, "ModelId", "Name");
            }
            return null;
        }

        public InventoryItemIndexPagingViewModel getAllInventoryItemPaging(int currentPage)
        {
            //int maxRows123 = 10;
            InventoryItemIndexPagingViewModel model = new InventoryItemIndexPagingViewModel();
            var InventoryItems = unitOfWork.InventoryItems.
                FindAll(c => /*c.LocationId == 2 &&*/ c.CurrentQuantity > 0, (currentPage - 1) * TablesMaxRows.InventoryItemIndex, TablesMaxRows.InventoryItemIndex , d=>d.CreateDts, OrderBy.Descending, includes: new[] { "InventoryItemType", "InventoryItemStatus", "Vendor" });
            model.items = mapper.Map<IEnumerable<InventoryItemIndexViewModel>>(InventoryItems).ToList();
            var itemsCount = unitOfWork.InventoryItems.Count(c => /*c.LocationId == 2 &&*/ c.CurrentQuantity > 0);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.InventoryItemIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.InventoryItemIndex;
            return model;
        }
        public InventoryItemTypeIndexPagingViewModel getAllInventoryItemTypePaging(int currentPage)
        {
            InventoryItemTypeIndexPagingViewModel viewModel = new InventoryItemTypeIndexPagingViewModel();
            var InventoryItemTypes = unitOfWork.InventoryItemType.
               FindAll(c => c.IsEnabled, (currentPage - 1) * TablesMaxRows.InventoryItemTypeIndex, TablesMaxRows.InventoryItemTypeIndex, d => d.CreateDts, OrderBy.Descending, includes: new[] { "InventoryItemTypeClassification", "InventoryItemTypeUnit", "Brand", "Model", "Warehouse" });
            viewModel.itemTypes = mapper.Map<IEnumerable<InventoryItemTypeIndexViewModel>>(InventoryItemTypes).ToList();
            var itemsCount = unitOfWork.InventoryItemType.Count(c => /*c.LocationId == 2 &&*/ c.IsEnabled );
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.InventoryItemTypeIndex));
            viewModel.PageCount = (int)Math.Ceiling(pageCount);
            viewModel.CurrentPageIndex = currentPage;
            viewModel.itemsCount = itemsCount;
            viewModel.Tablelength = TablesMaxRows.InventoryItemTypeIndex;           
            return viewModel;
        }
        public InventoryTablelPagingViewModel<InventoryItemStatus> getAllInventoryStatusPaging(int currentPage)
        {
            InventoryTablelPagingViewModel<InventoryItemStatus> viewModel = new InventoryTablelPagingViewModel<InventoryItemStatus>();
            var InventoryStatus = unitOfWork.InventoryItemStatus.
               FindAll(null, (currentPage - 1) * TablesMaxRows.InventoryStatusIndex, TablesMaxRows.InventoryStatusIndex, d => d.Name, OrderBy.Ascending);
            viewModel.items = InventoryStatus.ToList();
            var itemsCount = unitOfWork.InventoryItemStatus.Count();
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.InventoryStatusIndex));
            viewModel.PageCount = (int)Math.Ceiling(pageCount);
            viewModel.CurrentPageIndex = currentPage;
            viewModel.itemsCount = itemsCount;
            viewModel.Tablelength = TablesMaxRows.InventoryStatusIndex;
            return viewModel;
        }
        public InventoryTablelPagingViewModel<InventoryItemTypeUnit> getAllInventoryUnitsPaging(int currentPage)
        {
            InventoryTablelPagingViewModel<InventoryItemTypeUnit> viewModel = new InventoryTablelPagingViewModel<InventoryItemTypeUnit>();
            var InventoryUnits = unitOfWork.InventoryItemTypeUnits.
               FindAll(null, (currentPage - 1) * TablesMaxRows.InventoryUnitsIndex, TablesMaxRows.InventoryUnitsIndex, d => d.Name, OrderBy.Ascending);
            viewModel.items = InventoryUnits.ToList();
            var itemsCount = unitOfWork.InventoryItemTypeUnits.Count();
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.InventoryUnitsIndex));
            viewModel.PageCount = (int)Math.Ceiling(pageCount);
            viewModel.CurrentPageIndex = currentPage;
            viewModel.itemsCount = itemsCount;
            viewModel.Tablelength = TablesMaxRows.InventoryUnitsIndex;
            return viewModel;
        }
        public InventoryTablelPagingViewModel<InventoryItemTypeClassification> getAllInventoryClassificationsPaging(int currentPage)
        {
            InventoryTablelPagingViewModel<InventoryItemTypeClassification> viewModel = new InventoryTablelPagingViewModel<InventoryItemTypeClassification>();
            var InventoryClassification = unitOfWork.InventoryItemTypeClassifications.
               FindAll(null, (currentPage - 1) * TablesMaxRows.InventoryClassificationIndex, TablesMaxRows.InventoryClassificationIndex, d => d.Name, OrderBy.Ascending);
            viewModel.items = InventoryClassification.ToList();
            var itemsCount = unitOfWork.InventoryItemTypeClassifications.Count();
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.InventoryClassificationIndex));
            viewModel.PageCount = (int)Math.Ceiling(pageCount);
            viewModel.CurrentPageIndex = currentPage;
            viewModel.itemsCount = itemsCount;
            viewModel.Tablelength = TablesMaxRows.InventoryClassificationIndex;
            return viewModel;
        }
        public InventoryTablelPagingViewModel<Model> getAllInventoryModelsPaging(int currentPage)
        {
            InventoryTablelPagingViewModel<Model> viewModel = new InventoryTablelPagingViewModel<Model>();
            var InventoryModels = unitOfWork.Models.
               FindAll(null, (currentPage - 1) * TablesMaxRows.InventoryModelIndex, TablesMaxRows.InventoryModelIndex, d => d.Name, OrderBy.Ascending,new[] { "Brand"});
            viewModel.items = InventoryModels.ToList();
            var itemsCount = unitOfWork.Models.Count();
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.InventoryModelIndex));
            viewModel.PageCount = (int)Math.Ceiling(pageCount);
            viewModel.CurrentPageIndex = currentPage;
            viewModel.itemsCount = itemsCount;
            viewModel.Tablelength = TablesMaxRows.InventoryModelIndex;
            return viewModel;
        }

        public InventoryTablelPagingViewModel<Brand> getAllInventoryBrandsPaging(int currentPage)
        {
            InventoryTablelPagingViewModel<Brand> viewModel = new InventoryTablelPagingViewModel<Brand>();
            var Brands = unitOfWork.Brands.
               FindAll(null, (currentPage - 1) * TablesMaxRows.InventoryBrandIndex, TablesMaxRows.InventoryBrandIndex, d => d.Name, OrderBy.Ascending);
            viewModel.items = Brands.ToList();
            var itemsCount = unitOfWork.Brands.Count();
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.InventoryBrandIndex));
            viewModel.PageCount = (int)Math.Ceiling(pageCount);
            viewModel.CurrentPageIndex = currentPage;
            viewModel.itemsCount = itemsCount;
            viewModel.Tablelength = TablesMaxRows.InventoryBrandIndex;
            return viewModel;
        }
        public InventoryTablelPagingViewModel<Vendor> getAllVendorPaging(int currentPage)
        {
            InventoryTablelPagingViewModel<Vendor> viewModel = new InventoryTablelPagingViewModel<Vendor>();
            var Vendors = unitOfWork.Vendors.
               FindAll(null, (currentPage - 1) * TablesMaxRows.VendorIndex, TablesMaxRows.VendorIndex, d => d.Name, OrderBy.Ascending);
            viewModel.items = Vendors.ToList();
            var itemsCount = unitOfWork.Vendors.Count();
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.VendorIndex));
            viewModel.PageCount = (int)Math.Ceiling(pageCount);
            viewModel.CurrentPageIndex = currentPage;
            viewModel.itemsCount = itemsCount;
            viewModel.Tablelength = TablesMaxRows.VendorIndex;
            return viewModel;
        }

        //public void getFieldsbyWarehouseId(long warehouseId)
        //{
        //    if (warehouseId == 1)//101
        //    {
        //        var ParameterEntities = unitOfWork.ParameterEntities.GetAllWithCriteria(c => c.EntityId == 101);
        //    }
        //    else if (warehouseId == 2) //102
        //    {
        //        var ParameterEntities = unitOfWork.ParameterEntities.GetAllWithCriteria(c => c.EntityId == 102);

        //    }
        //    else if (warehouseId == 3)  //103
        //    {
        //        var ParameterEntities = unitOfWork.ParameterEntities.GetAllWithCriteria(c => c.EntityId == 103);
        //    }
        //}

        public SelectList getValuesTireSize()
        {
            var values = unitOfWork.TireSizes.GetAll();
            if (values.Count() > 0)
            {
                return new SelectList(values, "TireSizeId", "Name");
            }
            return null;
        }
        public SelectList getValuesTirePattern()
        {
            var values = unitOfWork.ParameterListValues.GetAllWithCriteria(c => c.ParameterId == 201);
            if (values.Count() > 0)
            {
                return new SelectList(values, "ParameterListValueId", "Value");
            }
            return null;
        }
        public SelectList getValuesViscosity()
        {
            var values = unitOfWork.ParameterListValues.GetAllWithCriteria(c => c.ParameterId==100);
            if (values.Count() > 0)
            {
                return new SelectList(values, "ParameterListValueId", "Value");
            }
            return null;
        }
        public InventoryItemIndexPagingViewModel getAllInventoryItemPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.InventoryItemIndex = length;
           return getAllInventoryItemPaging(currentPageIndex);
        }
        public InventoryItemTypeIndexPagingViewModel getAllInventoryItemTypePagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.InventoryItemTypeIndex = length;
            return getAllInventoryItemTypePaging(currentPageIndex);
        }
        public InventoryTablelPagingViewModel<InventoryItemStatus> getAllInventoryItemStatusPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.InventoryStatusIndex = length;
            return getAllInventoryStatusPaging(currentPageIndex);
        }
        public InventoryTablelPagingViewModel<InventoryItemTypeUnit> getAllInventoryUnitsPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.InventoryUnitsIndex = length;
            return getAllInventoryUnitsPaging(currentPageIndex);
        }
        public InventoryTablelPagingViewModel<InventoryItemTypeClassification> getAllInventoryClassificationPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.InventoryClassificationIndex = length;
            return getAllInventoryClassificationsPaging(currentPageIndex);
        }
        public InventoryTablelPagingViewModel<Model> getAllInventoryModelPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.InventoryModelIndex = length;
            return getAllInventoryModelsPaging(currentPageIndex);
        }
        public InventoryTablelPagingViewModel<Brand> getAllInventoryBrandPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.InventoryBrandIndex = length;
            return getAllInventoryBrandsPaging(currentPageIndex);
        }
        public InventoryTablelPagingViewModel<Vendor> getAllVendorPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.VendorIndex = length;
            return getAllVendorPaging(currentPageIndex);
        }
        public SelectList GetSubWarehouseStructure(long warehouseId)
        {
          var inventoryLocationLevel = unitOfWork.Warehouse.GetOne( c => c.WarehouseId == warehouseId  &&c.InventoryLocationLevelId.HasValue, new[] { "InventoryLocationLevel" });
            if (inventoryLocationLevel !=null && inventoryLocationLevel.InventoryLocationLevelId.HasValue)
            {
                var inventoryLocationLevelId = inventoryLocationLevel.InventoryLocationLevelId.Value;
                var Locations = unitOfWork.InventoryLocation.GetAllWithCriteria(c => c.InventoryLocationLevelId == inventoryLocationLevelId && !c.ParentInventoryLocationId.HasValue && c.WarehouseId == warehouseId);
                if (Locations.Count() > 0)
                {
                    return new SelectList(Locations, "InventoryLocationId", "Name");
                }
            }
           
          return null;
        }
        public SelectList GetLaneWarehouseStructure(long subWarehouseId)
        {
            var Locations = unitOfWork.InventoryLocation.GetAllWithCriteria(c => c.ParentInventoryLocationId.HasValue && c.ParentInventoryLocationId.Value == subWarehouseId);
            if (Locations.Count() > 0)
            {
                return new SelectList(Locations, "InventoryLocationId", "Name");
            }
            return null;
        }
        public SelectList GetShelfWarehouseStructure(long laneWarehouseId)
        {
            var Locations = unitOfWork.InventoryLocation.GetAllWithCriteria(c => c.ParentInventoryLocationId.HasValue && c.ParentInventoryLocationId.Value == laneWarehouseId);
            if (Locations.Count() > 0)
            {
                return new SelectList(Locations, "InventoryLocationId", "Name");
            }
            return null;
        }
        public SelectList GetWarehouseTypes(long WarehouseId)
        {
            var Classifications = unitOfWork.WarehouseInventoryItemTypeClassification.GetAllWithCriteria(c => c.WarehouseId == WarehouseId).Select(c=>c.InventoryItemTypeClassificationId).ToList();

            var types = unitOfWork.InventoryItemType.GetAllWithCriteria(c=> Classifications.Any(b => b == c.InventoryItemTypeClassificationId));
            if ( types != null && types.Count() > 0 )
            {
                return new SelectList(types, "InventoryItemTypeId", "Name");
            }
            return null;
        }
        public SelectList GetItemTypeStatus(long itemType)
        {
            var StatusIds = unitOfWork.InventoryItemStatusInventoryItemType.GetAllWithCriteria(c => c.InventoryItemTypeId == itemType).Select(c => c.InventoryItemStatusId).ToList();

            var Status =  unitOfWork.InventoryItemStatus.GetAllWithCriteria(c => StatusIds.Any(b => b == c.InventoryItemStatusId)).ToList();
            if (Status != null && Status.Count() > 0)
            {
                return new SelectList(Status, "InventoryItemStatusId", "Name");
            }
            return null;
        }
        public List<InventoryItemStatus> GetItemTypeStatusList(long itemType)
        {
            var StatusIds = unitOfWork.InventoryItemStatusInventoryItemType.GetAllWithCriteria(c => c.InventoryItemTypeId == itemType).Select(c => c.InventoryItemStatusId).ToList();

            var Status = unitOfWork.InventoryItemStatus.GetAllWithCriteria(c => StatusIds.Any(b => b == c.InventoryItemStatusId)).ToList();
            
            return Status;
        }
        public List<InventoryItemStatus> GetStatusNotContainItemType(int itemType)
        {
           
                var existsStatus = unitOfWork.InventoryItemStatusInventoryItemType.GetAllWithCriteria(c => c.InventoryItemTypeId == itemType).Select(c => c.InventoryItemStatusId).ToList();
                var Status = unitOfWork.InventoryItemStatus.GetAllWithCriteria(c => !existsStatus.Any(b => b == c.InventoryItemStatusId));

                if (Status.Count() > 0)
                {
                    var Status2 = Status.OrderBy(c => c.Name).ToList();
                    return Status2;
                }
            return new List<InventoryItemStatus>();


        }
        public SelectList GetBrands()
        {
            var Brands = unitOfWork.Brands.GetAll();
            if (Brands != null && Brands.Count() > 0)
            {
                var Brands2 = Brands.OrderBy(c => c.Name);
                return new SelectList(Brands2, "BrandId", "Name");
            }
            return null;
        }

        public SelectList GetBrands(Brand SelectedValues)
        {
            var brands = unitOfWork.Brands.GetAll();
            if (brands.Count() > 0)
            {
                return new SelectList(brands, "BrandId", "Name", SelectedValues);
            }
            return null;
        }
        public List<Brand> GetListBrands()
        {
            return  unitOfWork.Brands.GetAll().ToList();
           
           
        }
        public SelectList GetItemtypeCodetypes(long itemType)
        {
            var codeTypes = unitOfWork.CodeTypes.GetAll();
            if (codeTypes != null && codeTypes.Count() > 0)
            {
                return new SelectList(codeTypes, "CodeTypeId", "Name");
            }
            return null;
        }

        public SelectList GetItemtypeVendors(long itemType)
        {
            var vendors = unitOfWork.Vendors.GetAll();
            if (vendors != null && vendors.Count() > 0)
            {
                return new SelectList(vendors, "VendorId", "Name");
            }
            return null;
        }
        public SelectList GetItemtypeVendors()
        {
            var vendors = unitOfWork.Vendors.GetAll();
            if (vendors != null && vendors.Count() > 0)
            {
                return new SelectList(vendors, "VendorId", "Name");
            }
            return null;
        }
        public bool ItemTypeIsAutoGenerateSerial(long itemType)
        {
          return unitOfWork.InventoryItemType.GetAllWithCriteria(c=>c.InventoryItemTypeId == itemType).FirstOrDefault().AutoGenerateSerial;          
        }
        public bool ItemTypeIsQuantity(long itemType)
        {
            return unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.InventoryItemTypeId == itemType).FirstOrDefault().IsQuantitative;
        }
    }
}
