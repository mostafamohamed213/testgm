using AutoMapper;
using CGARMAN.ViewModel;
using CGARMAN.ViewModel.Vehicle;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Services
{
    public class VehicleServices
    {
        IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public VehicleServices(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;        
        }
        public PagingViewModel<Vehicle> getAllVehiclesPaging(int currentPage, string plateNumber = null , int departmentID = 0, int ownerID =0 , int statusID =0, int familyID =0, int brandID=0)
        {
            PagingViewModel<Vehicle> model = new PagingViewModel<Vehicle>();
            var vehicles = unitOfWork.Vehicle.FindAll(c => c.Enable
            && (!string.IsNullOrEmpty(plateNumber) ? c.LicenseNumber.ToLower().Contains(plateNumber.ToLower()) : true)           
            && ( departmentID > 0 ? departmentID == c.VehicleDepartmentId : true)
            &&( ownerID > 0 ? ownerID == c.VehicleOwnerId : true )
            &&( statusID > 0 ? statusID == c.VehicleStatusId : true)
            &&( familyID > 0 ? familyID == c.VehicleFamilyId : true)
            &&( brandID > 0 ? brandID == c.VehicleBrandId : true )
            , (currentPage - 1) * TablesMaxRows.VehicleIndex, TablesMaxRows.VehicleIndex, d => d.CreateDts, OrderBy.Descending, new[] { "VehicleFamily", "VehicleBrand", "VehicleOwner" , "VehicleStatus", "VehicleDepartment"});           
            model.items = vehicles.ToList();
            var itemsCount = unitOfWork.Vehicle.Count(c => c.Enable && (!string.IsNullOrEmpty(plateNumber) ? c.LicenseNumber.ToLower().Contains(plateNumber.ToLower()) : true)
            && (departmentID > 0 ? departmentID == c.VehicleDepartmentId : true)
            && (ownerID > 0 ? ownerID == c.VehicleOwnerId : true)
            && (statusID > 0 ? statusID == c.VehicleStatusId : true)
            && (familyID > 0 ? familyID == c.VehicleFamilyId : true)
            && (brandID > 0 ? brandID == c.VehicleBrandId : true)
            );
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.VehicleIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.VehicleIndex;
            return model;
        }
        public PagingViewModel<Vehicle> getAllVehiclesPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.VehicleIndex = length;
            return getAllVehiclesPaging(currentPageIndex);
        }
        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && c.LicenseNumber.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleId, label = c.LicenseNumber }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && !c.LicenseNumber.ToLower().StartsWith(prefix.ToLower()) && c.LicenseNumber.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleId, label = c.LicenseNumber }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }
        internal List<AutoCompleteViewModel> AutoCompleteAddTire(string prefix)
        {
            var ItemTypeIds = unitOfWork.InventoryItemType.GetAllWithCriteria(c => c.Name.ToLower().Contains("tire")).Select(c=>c.InventoryItemTypeId);
            List<AutoCompleteViewModel> values =
                unitOfWork.InventoryItems.GetAllWithCriteria(c => ItemTypeIds.Contains(c.InventoryItemTypeId) && c.SerialNumber.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.InventoryItemId, label = c.SerialNumber }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.InventoryItems.GetAllWithCriteria(c => ItemTypeIds.Contains(c.InventoryItemTypeId) && !c.SerialNumber.ToLower().StartsWith(prefix.ToLower()) && c.SerialNumber.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.InventoryItemId, label = c.SerialNumber }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }
        internal List<AutoCompleteViewModel> AutoCompleteLinkTrailer(string prefix)
        {
           var families =  unitOfWork.VehicleFamily.GetAllWithCriteria(c => c.Enable && (c.Name.Trim().ToLower().Contains("silo") || c.Name.Trim().ToLower().Contains("trailer"))).Select(c=>c.VehicleFamilyId).ToList();
            List<AutoCompleteViewModel> values =
                unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && families.Contains(c.VehicleFamilyId) &&c.LicenseNumber.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleId, label = c.LicenseNumber }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && families.Contains(c.VehicleFamilyId) && !c.LicenseNumber.ToLower().StartsWith(prefix.ToLower()) && c.LicenseNumber.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleId, label = c.LicenseNumber }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }
        internal string getFamilyNameById(int vehicleFamilyId)
        {
           return unitOfWork.VehicleFamily.GetOne(c => c.VehicleFamilyId == vehicleFamilyId).Name.Trim();
        }

        internal long Create(VehicleCreateViewModel model)
        {
            Vehicle vehicle = mapper.Map<Vehicle>(model);
            DateTime now = DateTime.Now;
            vehicle.CreateDts = now;
            vehicle.SystemUserCreate = "1";
            vehicle.Enable = true;
           
            VehicleLicense vehicleLicense = new VehicleLicense() {
                VehicleId = vehicle.VehicleId,
                LicenseNumber = model.LicenseNumber,
                StartDate = now,
                EndDate = null,
                CreateDts = now,
                Enable = true,
                SystemUserCreate = "1"
            };
            vehicle.VehicleLicenses.Add(vehicleLicense);
            unitOfWork.Vehicle.Add(vehicle);
            unitOfWork.Complete();
            return vehicle.VehicleId;
        }
        internal long  Edit(VehicleCreateViewModel model)
        {
            Vehicle vehicle = unitOfWork.Vehicle.GetOne(c => c.VehicleId == model.VehicleId);
            if (vehicle is not null)
            {
                vehicle.VehicleDepartmentId = model.VehicleDepartmentId;
                vehicle.VehicleOwnerId = model.VehicleOwnerId;
                vehicle.VehicleFamilyId = model.VehicleFamilyId;
                vehicle.VehicleBrandId = model.VehicleBrandId;
                vehicle.Capacity = model.Capacity;
                vehicle.ChassisType = model.ChassisType;
                vehicle.ChassisSerial = model.ChassisSerial;
                vehicle.EngineType = model.EngineType;
                vehicle.EngineSerial = model.EngineSerial;
                vehicle.TireSizeId = model.TireSizeId;
                vehicle.ManufacturingYear = model.ManufacturingYear;
                vehicle.CostCenterId = model.CostCenterId;
                vehicle.VehicleStatusId = model.VehicleStatusId;
                vehicle.TiresCount = model.TiresCount;
                vehicle.Notes = model.Notes;

                var currentLicense = unitOfWork.VehicleLicense.GetOne(c => !c.EndDate.HasValue && c.VehicleId == vehicle.VehicleId);
                if (currentLicense is not null)
                {
                    if (currentLicense.LicenseNumber.Trim() != model.LicenseNumber.Trim() )
                    {
                        DateTime now = DateTime.Now;
                        currentLicense.EndDate = now;
                        VehicleLicense NewLicense = new VehicleLicense()
                        {
                            VehicleId = vehicle.VehicleId,
                            LicenseNumber = model.LicenseNumber,
                            StartDate = now,
                            EndDate = null,
                            CreateDts = now,
                            Enable = true,
                            SystemUserCreate = "1"
                        };
                        vehicle.VehicleLicenses.Add(NewLicense);                        
                    }
                }
                vehicle.LicenseNumber = model.LicenseNumber;
                unitOfWork.Complete();
                return vehicle.VehicleId;
            }
            return 0;
        }

        internal Vehicle GetVehicleById(long vehicleId)
        {
            return unitOfWork.Vehicle.GetOne(c=>c.VehicleId == vehicleId);
        }

        internal List<VehicleAttachment> GetTrailerHistory(long vehicleId)
        {            
          return unitOfWork.VehicleAttachment.GetAllWithCriteria(c => c.VehicleId == vehicleId || c.AttachedVehicleId == vehicleId,new[] { "AttachedVehicle", "Vehicle" }).OrderBy(c=>c.CreateDts).ToList();
        }

        internal long LinkTrailer(long vehicleID, long trailerID)
        {
            var vehicle = unitOfWork.Vehicle.GetOne(c => c.Enable && c.VehicleId == vehicleID);
            var trailer = unitOfWork.Vehicle.GetOne(c => c.Enable && c.VehicleId == trailerID);
            if (vehicle is not null && trailer is not null)
                {
               var oldAttch= unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && c.AttachedVehicleId.HasValue && c.AttachedVehicleId == vehicle.VehicleId);
                if (oldAttch is not null && oldAttch.Count()>0)
                {
                    foreach (var item in oldAttch)
                    {
                        item.AttachedVehicleId = null;
                    }
                }
                VehicleAttachment vehicleAttachment = unitOfWork.VehicleAttachment.GetOne(v => v.VehicleId == vehicle.VehicleId && !v.EndDate.HasValue);
                VehicleAttachment trailerAttachment = unitOfWork.VehicleAttachment.GetOne(v => v.VehicleId == trailer.VehicleId && !v.EndDate.HasValue);

                DateTime now = DateTime.Now.Date;
                if (vehicleAttachment != null)
                {
                    vehicleAttachment.EndDate = now;                   
                }
                if (trailerAttachment != null)
                {
                    trailerAttachment.EndDate = now;
                }

                trailer.AttachedVehicleId = vehicle.VehicleId;
                vehicle.AttachedVehicleId = trailer.VehicleId;

                VehicleAttachment attachment = new VehicleAttachment();
                attachment.StartDate = now;
                attachment.EndDate = null;
                attachment.VehicleId = vehicle.VehicleId;
                attachment.AttachedVehicleId = trailer.VehicleId;
                unitOfWork.VehicleAttachment.Add(attachment);
                unitOfWork.Complete();
                return vehicle.VehicleId;
            }
            return 0;
        }

        //internal PagingViewModel<Vehicle> AdvancedSearch( string plateNumber, int departmentID, int ownerID, int statusID, int familyID, int brandID)
        //{
        //    var Vehicles = unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable
        //    && !string.IsNullOrEmpty(plateNumber) ? c.LicenseNumber.ToLower().Contains(plateNumber.ToLower()) : true 
        //    && departmentID > 0 ?departmentID ==c.VehicleDepartmentId : true
        //    && ownerID > 0 ? ownerID == c.VehicleOwnerId : true
        //    && statusID > 0 ? statusID == c.VehicleStatusId : true
        //    && familyID > 0 ? familyID == c.VehicleFamilyId : true
        //    && brandID > 0 ? brandID == c.VehicleBrandId : true
        //    , new[] { "VehicleFamily", "VehicleBrand", "VehicleOwner", "VehicleStatus", "VehicleDepartment" });
        //    PagingViewModel<Vehicle> model = new PagingViewModel<Vehicle>();
        //    model.items = Vehicles.ToList();
        //    var itemsCount = Vehicles.Count(c => c.Enable
        //    && !string.IsNullOrEmpty(plateNumber) ? c.LicenseNumber.ToLower().Contains(plateNumber.ToLower()) : true
        //    && departmentID > 0 ? departmentID == c.VehicleDepartmentId : true
        //    && ownerID > 0 ? ownerID == c.VehicleOwnerId : true
        //    && statusID > 0 ? statusID == c.VehicleStatusId : true
        //    && familyID > 0 ? familyID == c.VehicleFamilyId : true
        //    && brandID > 0 ? brandID == c.VehicleBrandId : true
        //    );
        //    double pageCount = (double)(itemsCount / Convert.ToDecimal(100));
        //    model.PageCount = (int)Math.Ceiling(pageCount);
        //    model.CurrentPageIndex = 1;
        //    model.itemsCount = itemsCount;
        //    model.Tablelength = 100;
        //    return model;
        //}
        internal bool SearchIfLicenseNumberExists(string LicenseNumber, long id = 0)
        {
            return unitOfWork.Vehicle.GetOne(c => c.Enable && c.LicenseNumber.ToLower().Trim() == LicenseNumber.ToLower().Trim() && c.VehicleId != id) == null ? true : false;
        }
        //internal bool ValidateplateNumberIslinkedByAnotherVehicle(string LicenseNumber)
        //{
        //    return unitOfWork.Vehicle.GetOne(c => c.LicenseNumber.ToLower().Trim() == LicenseNumber.ToLower().Trim() && c.AttachedVehicleId != VehicleId) == null ? true : false;
        //}
        internal bool ValidateplateNumberExistsAndFree(string LicenseNumber)
        {
            var License=unitOfWork.VehicleLicense.GetOne(c => c.Enable && c.LicenseNumber.Trim() == LicenseNumber.Trim() && !c.EndDate.HasValue);
            if (License is not null)
            {
                // var trailer= unitOfWork.Vehicle.GetOne(c => c.VehicleId == License.VehicleId && !c.AttachedVehicleId.HasValue ) == null ?  false : true;
                var trailer= unitOfWork.Vehicle.GetOne(c => c.Enable && c.VehicleId == License.VehicleId && !c.AttachedVehicleId.HasValue, new[] { "VehicleFamily" } );
                if (trailer is not null && (trailer.VehicleFamily.Name.ToLower().Trim().Contains("trailer") || trailer.VehicleFamily.Name.ToLower().Trim().Contains("silo")))
                {
                    return true;
                }
            }
            return false;
        }

        internal int SaveTire(long vehicleId, int tirePosition, string serial)
        {
          var InventoryItem =  unitOfWork.InventoryItems.GetOne(c => c.SerialNumber == serial.Trim() && c.InventoryItemTypeId == 40);
            var Vehicle = unitOfWork.Vehicle.GetOne(c=>c.VehicleId == vehicleId && c.Enable);
            if (InventoryItem is not null && Vehicle is not null)
            {
                var oldTire = unitOfWork.VehicleTire.GetOne(c => c.VehicleId == vehicleId && c.TirePosition == tirePosition && !c.EndDts.HasValue);
                if (oldTire is not null && oldTire.InventoryItemId == InventoryItem.InventoryItemId)
                {
                    return 1;
                }
                if (InventoryItem.CurrentQuantity>0)
                {
                    DateTime now = DateTime.Now;
                    InventoryItemAssignment itemAssignment = new InventoryItemAssignment()
                    {
                        AssignmentDts = now,
                        InventoryItemId = InventoryItem.InventoryItemId,
                        ObjectId = Vehicle.VehicleId,
                        Quantity =1
                    };
                    unitOfWork.InventoryItemAssignment.Add(itemAssignment);
                    decimal oldCurrentQuantity = InventoryItem.CurrentQuantity;
                    InventoryItem.CurrentQuantity--;
                    InventoryItem.InventoryItemStatusId = 4;
                    InventoryLog log = new InventoryLog()
                    {
                        CreateDT = now,
                        Description = "add tire to vechile and update CurrentQuantity",
                        InventoryLogOperationID = 1,
                        InventoryLogTableID = 12,
                        SystemUserID = "1",
                        ObjectID = InventoryItem.InventoryItemId,
                        Object1 = $"OldCurrentQuantity = {oldCurrentQuantity} , NewCurrentQuantity = {InventoryItem.CurrentQuantity}"

                    };
                    unitOfWork.InventoryLog.Add(log);
                    InventoryItemStatusLog statusLog = new InventoryItemStatusLog
                    {
                        InventoryItem = InventoryItem,
                        InventoryItemStatusId = InventoryItem.InventoryItemStatusId,
                        StatusDts = now
                    };
                    unitOfWork.InventoryItemStatusLog.Add(statusLog);
                    //var  vt=  unitOfWork.VehicleTire.GetOne(c=>c.VehicleId == vehicleId && c.TirePosition == tirePosition && !c.EndDts.HasValue);
                    if (oldTire is not null && oldTire.InventoryItemId != InventoryItem.InventoryItemId)
                    {
                        oldTire.EndDts = now;
                        ReturnTireToInventory(oldTire.InventoryItemId, oldTire.VehicleId);
                    }                   
                    unitOfWork.VehicleTire.Add(new VehicleTire
                    {
                        InventoryItemId = InventoryItem.InventoryItemId,
                        Pressure = 0,
                        TirePosition = tirePosition,
                        StartDts = now,
                        TireTreadDepthA = 0,
                        TireTreadDepthB = 0,
                        TireTreadDepthC = 0,
                        VehicleId = vehicleId
                    });
           
                    unitOfWork.Complete();
                    return 1;
                }
                return -3;
            }
            return -2;
        }

        internal List<MaintenanceItem> PendingTasks(long vehicleId)
        {
            //WMSContext _context = new WMSContext();
            //long? lastWO = _context.WorkOrders.Where(w => w.VehicleId == vehicleId).Max(w => (long?)w.WorkOrderNumber);
            var wos = unitOfWork.WorkOrder.GetAllWithCriteria(c => c.VehicleId == vehicleId);
            long? lastWO = wos is not null && wos.Count()>0 ? wos.Max(c => c.WorkOrderNumber) : null;
              
            List<MaintenanceItem> items;
            if (lastWO.HasValue && lastWO.Value > 0 )
            {
                var woIds = unitOfWork.WorkOrder.GetAllWithCriteria(c => c.VehicleId == vehicleId && c.WorkOrderNumber == lastWO).Select(c=>c.WorkOrderNumber).ToList();
                var mIds = unitOfWork.Maintenance.GetAllWithCriteria(c => woIds.Contains(c.WorkOrderNumber)).Select(c=>c.MaintenanceId).ToList();
                items = unitOfWork.MaintenanceItem.GetAllWithCriteria(c => c.MaintenanceItemStatusId == 1 && !c.EndTime.HasValue && mIds.Contains(c.MaintenanceId),new[] { "MaintenanceItemType", "MaintenanceAction", "Technician" , "Maintenance" }).ToList();
                //items = (from mi in _context.MaintenanceItems
                //         join m in _context.Maintenances on mi.MaintenanceId equals m.MaintenanceId
                //         join w in _context.WorkOrders on m.WorkOrderNumber equals w.WorkOrderNumber
                //         where w.VehicleId == vehicleId  && w.WorkOrderNumber == lastWO && !mi.EndTime.HasValue && mi.MaintenanceItemStatusId == 1
                //         select mi)
                //             .Include(m => m.MaintenanceAction)
                //             .Include(m => m.Technician)
                //             .Include(m => m.MaintenanceItemType)
                //             .ToList();
            }
            else
            {
                items = new List<MaintenanceItem>();
            }
            return items;
        }

        internal int DeleteTire(long inventoryItemId, long vehicleId, int position)
        {
            var tire = unitOfWork.VehicleTire.GetOne(c => c.VehicleId == vehicleId && c.TirePosition == position && c.InventoryItemId == inventoryItemId && !c.EndDts.HasValue);
            if (tire is not null )
            {
                tire.EndDts = DateTime.Now;
              return  ReturnTireToInventory(inventoryItemId, vehicleId);
            }
            return -1;
        }

        internal int ReturnTireToInventory(long InventoryItemId ,long VehicleID)
        {
           

            var InventoryItem = unitOfWork.InventoryItems.GetOne(c => c.InventoryItemId == InventoryItemId);
            if (InventoryItem is not null)
            {
                DateTime now = DateTime.Now;
                decimal oldCurrentQuantity = InventoryItem.CurrentQuantity;
                InventoryItem.CurrentQuantity++;
                var ItemAssignment =   unitOfWork.InventoryItemAssignment.GetOne(c=>c.ObjectId==VehicleID && c.InventoryItemId == InventoryItemId && !c.EndDateTime.HasValue );
                ItemAssignment.EndDateTime = now;
                InventoryItem.InventoryItemStatusId = 6;
                InventoryItemStatusLog statusLog = new InventoryItemStatusLog
                {
                    InventoryItem = InventoryItem,
                    InventoryItemStatusId = InventoryItem.InventoryItemStatusId,
                    StatusDts = now
                };
                unitOfWork.InventoryItemStatusLog.Add(statusLog);
                InventoryLog log = new InventoryLog()
                {
                    CreateDT = now,
                    Description = "Remove tire From vechile then return this to inventory and update CurrentQuantity",
                    InventoryLogOperationID = 2,
                    InventoryLogTableID = 12,
                    SystemUserID = "1",
                    ObjectID = InventoryItem.InventoryItemId,
                    Object1 = $"OldCurrentQuantity = {oldCurrentQuantity} , NewCurrentQuantity = {InventoryItem.CurrentQuantity}"
                };
                unitOfWork.InventoryLog.Add(log);
                unitOfWork.Complete();
                return 1;
            }
            return -1;
        }
        internal int UnlinkTrailer(long vehicleId, long trailerId)
        {
           var vehicle = unitOfWork.Vehicle.GetOne(c => c.VehicleId == vehicleId && c.Enable);
           var trailer = unitOfWork.Vehicle.GetOne(c => c.VehicleId == trailerId && c.Enable);
            if (vehicle is not null && trailer is not null )
            {
               var attach = unitOfWork.VehicleAttachment.GetAllWithCriteria(c=> (c.VehicleId == vehicleId || c.AttachedVehicleId == trailerId) && !c.EndDate.HasValue).ToList();
                if (attach is not null && attach.Count > 0)
                {
                    DateTime now = DateTime.Now;
                    foreach (var item in attach)
                    {
                        item.EndDate = now;
                    }
                }
               
                trailer.AttachedVehicleId = null;
                vehicle.AttachedVehicleId = null;
                unitOfWork.Complete();
                return 1;
            }
            return -1;
        }

        internal int Delete(long vehicleId)
        {
            var vehicle = unitOfWork.Vehicle.GetOne(c=> c.VehicleId == vehicleId && c.Enable );
            if (vehicle is not null)
            {
                vehicle.AttachedVehicleId = null;
                var existsAttch = unitOfWork.Vehicle.GetAllWithCriteria(c => c.AttachedVehicleId.HasValue && c.AttachedVehicleId == vehicle.VehicleId);
                if (existsAttch is not null && existsAttch.Count() > 0)
                {
                    foreach (var item in existsAttch)
                    {
                        item.AttachedVehicleId = null;
                    }
                }

                var attach = unitOfWork.VehicleAttachment.GetAllWithCriteria(c => (c.VehicleId == vehicleId || c.AttachedVehicleId == vehicleId) && !c.EndDate.HasValue).ToList();
                if (attach is not null && attach.Count > 0)
                {
                    DateTime now = DateTime.Now;
                    foreach (var item in attach)
                    {
                        item.EndDate = now;
                    }
                }

                var vehicleLicense = unitOfWork.VehicleLicense.GetAllWithCriteria(c => c.VehicleId == vehicleId  && c.Enable).ToList();
                if (vehicleLicense is not null && vehicleLicense.Count > 0)
                {
                    DateTime now = DateTime.Now;
                    foreach (var item in vehicleLicense)
                    {
                        if (!item.EndDate.HasValue)
                        {
                            item.EndDate = now;
                        }
                        item.Enable = false;
                    }
                }
                vehicle.Enable = false;              
                unitOfWork.Complete();
                return 1;
            }
            return -1;
        }

        internal bool ValidateplateNumberExistsAndFree2(long VehicleId)
        {
            var trailer = unitOfWork.Vehicle.GetOne(c => c.Enable && c.VehicleId == VehicleId && !c.AttachedVehicleId.HasValue, new[] { "VehicleFamily" });
            if (trailer is not null && (trailer.VehicleFamily.Name.ToLower().Trim().Contains("trailer") || trailer.VehicleFamily.Name.ToLower().Trim().Contains("silo")))
            {
                return true;
            }
            return false;
        }
        internal SelectList GetFamilies(int FamilyId = 0)
        {
            var values = unitOfWork.VehicleFamily.GetAllWithCriteria(c => c.Enable);
            if (values.Count() > 0)
            {
                if (FamilyId > 0)
                {
                    return new SelectList(values, "VehicleFamilyId", "Name", FamilyId);
                }
                return new SelectList(values, "VehicleFamilyId", "Name");
            }
            return null;
        }
        internal SelectList GetFamiliesForlinkTrailer(int FamilyId = 0)
        {
            var values = unitOfWork.VehicleFamily.GetAllWithCriteria(c => c.Enable && ( c.Name.ToLower().Trim().Contains("trailer") || c.Name.ToLower().Trim().Contains("silo") ));
            if (values.Count() > 0)
            {
                if (FamilyId > 0)
                {
                    return new SelectList(values, "VehicleFamilyId", "Name", FamilyId);
                }
                return new SelectList(values, "VehicleFamilyId", "Name");
            }
            return null;
        }
        internal SelectList GetDepartments(int DepartmentId =0)
        {
            var values = unitOfWork.VehicleDepartment.GetAllWithCriteria(c => c.Enable);
            if (values.Count() > 0)
            {
                if (DepartmentId > 0)
                {
                    return new SelectList(values, "VehicleDepartmentId", "Name", DepartmentId);
                }
                return new SelectList(values, "VehicleDepartmentId", "Name");
            }
            return null;
        }

        internal SelectList GetBrandsByfamilyId(int FamilyId, int BrandId = 0)
        {
            var values = unitOfWork.VehicleBrand.GetAllWithCriteria(c => c.Enable && c.VehicleFamilyId == FamilyId);
            if (values.Count() > 0)
            {
                if (BrandId > 0)
                {
                    return new SelectList(values, "VehicleBrandId", "Name", BrandId);
                }
                return new SelectList(values, "VehicleBrandId", "Name");
            }
            return null;
        }

        internal SelectList getPlateNumberByBrandId(int BrandId, int VehicleId = 0)
        {
            var families = unitOfWork.VehicleFamily.GetAllWithCriteria(c => c.Enable && (c.Name.Trim().ToLower().Contains("silo") || c.Name.Trim().ToLower().Contains("trailer"))).Select(c => c.VehicleFamilyId).ToList();

            var values = unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && families.Contains(c.VehicleFamilyId) && c.VehicleBrandId == BrandId);
            if (values.Count() > 0)
            {
                if (VehicleId > 0)
                {
                    return new SelectList(values, "VehicleId", "LicenseNumber", VehicleId);
                }
                return new SelectList(values, "VehicleId", "LicenseNumber");
            }
            return null;
        }
        internal SelectList GetStatus(int StatusId = 0)
        {
            var values = unitOfWork.VehicleStatus.GetAllWithCriteria(c => c.Enable);
            if (values.Count() > 0)
            {
                if (StatusId > 0)
                {
                    return new SelectList(values, "VehicleStatusId", "Name", StatusId);
                }
                return new SelectList(values, "VehicleStatusId", "Name");
            }
            return null;
        }
        internal SelectList GetOwners(int OwnerId = 0)
        {
            var values = unitOfWork.VehicleOwner.GetAllWithCriteria(c => c.Enable);
            if (values.Count() > 0)
            {
                if (OwnerId > 0)
                {
                    return new SelectList(values, "VehicleOwnerId", "Name", OwnerId);
                }
                return new SelectList(values, "VehicleOwnerId", "Name");
            }
            return null;
        }
        internal SelectList GetTireSizes(int TireSizeId = 0)
        {
            var values = unitOfWork.TireSizes.GetAllWithCriteria(c => c.Enable);
            if (values.Count() > 0)
            {
                if (TireSizeId > 0)
                {
                    return new SelectList(values, "TireSizeId", "Name", TireSizeId);
                }
                return new SelectList(values, "TireSizeId", "Name");
            }
            return null;
        }
        internal SelectList GetCostCenters(int CostCenteId = 0)
        {
            var values = unitOfWork.CostCenters.GetAllWithCriteria(c => c.Enable);
            if (values.Count() > 0)
            {
                if (CostCenteId > 0)
                {
                    return new SelectList(values, "CostCenterId", "Name", CostCenteId);
                }
                return new SelectList(values, "CostCenterId", "Name");
            }
            return null;
        }
        //internal SelectList ManufacturingYears(int manufacturingYear = 0)
        //{
        //    int endYear = DateTime.Now.Year + 3;
        //    List<int> years = new List<int>();
        //    for (int startYear = 1970; startYear < endYear; startYear++)
        //    {
        //        years.Add(startYear);
        //    }
        //    if (manufacturingYear > 0)
        //    {
        //        return new SelectList(years, manufacturingYear);
        //    }
        //    return new SelectList(years);           
        //}
        internal PagingViewModel<Vehicle> Search(string Name, int currentPage)
        {
            var Vehicles = unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.LicenseNumber.ToLower().Contains(Name.ToLower()) : false, new[] { "VehicleFamily", "VehicleBrand", "VehicleOwner", "VehicleStatus", "VehicleDepartment" });
            PagingViewModel<Vehicle> model = new PagingViewModel<Vehicle>();
            model.items = Vehicles.ToList();
            var itemsCount = Vehicles.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }
        internal VehicleCreateViewModel PrepareCreate(VehicleCreateViewModel model)
        {
            model.Status = GetStatus(model.VehicleStatusId);
            model.Owners = GetOwners(model.VehicleOwnerId);
            model.Departments = GetDepartments(model.VehicleDepartmentId);
            model.Families = GetFamilies(model.VehicleFamilyId);
            model.TireSizes = GetTireSizes(model.TireSizeId);
            model.CostCenters = GetCostCenters(model.TireSizeId);
            return model;
        }
        internal VehicleCreateViewModel getVehicleViewModelById(int vehicleId)
        {
            Vehicle vehicle =  unitOfWork.Vehicle.GetOne(c => c.VehicleId == vehicleId);
            VehicleCreateViewModel model = mapper.Map<VehicleCreateViewModel>(vehicle);
            PrepareCreate(model);
            return model;
        }
        internal VehicleViewViewModel ViewVehicle(long vehicleId)
        {
            Vehicle vehicle = unitOfWork.Vehicle.GetOne(c => c.VehicleId == vehicleId, new[] {"VehicleLicenses", "CostCenter", "TireSize", 
                "VehicleFamily", "VehicleBrand", "VehicleOwner", "VehicleStatus", "VehicleDepartment",
                "AttachedVehicle", "AttachedVehicle.VehicleDepartment" ,"AttachedVehicle.VehicleOwner","AttachedVehicle.VehicleFamily",
                "AttachedVehicle.VehicleBrand","AttachedVehicle.CostCenter","AttachedVehicle.VehicleStatus","AttachedVehicle.TireSize"
            });
            VehicleViewViewModel model = mapper.Map<VehicleViewViewModel>(vehicle);
            if (vehicle.VehicleFamily.Name.Trim().ToLower().Contains("silo") || vehicle.VehicleFamily.Name.Trim().ToLower().Contains("trailer"))
            {
                model.isVehicle = false;
            }
            else
            {
                model.isVehicle = true;
            }
            model.vehicleCurrentTire = GetVehicleCurrentTireByVehicleId(vehicleId);
            return model;

        }
        internal List<VehicleCurrentTire> GetVehicleCurrentTireByVehicleId(long vehicleId)
        {
           return unitOfWork.VehicleCurrentTire.GetAllWithCriteria(c => c.VehicleId == vehicleId).OrderBy(c => c.TirePosition).ToList();

        }
    }
}
