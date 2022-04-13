using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            InventoryItemHistories = new HashSet<InventoryItemHistory>();
            Schedules = new HashSet<Schedule>();
            TireTests = new HashSet<TireTest>();
            VehicleAttachmentAttachedVehicles = new HashSet<VehicleAttachment>();
            VehicleAttachmentVehicles = new HashSet<VehicleAttachment>();
            VehicleLicenses = new HashSet<VehicleLicense>();
            WorkOrders = new HashSet<WorkOrder>();
        }

        public long VehicleId { get; set; }
        public long? AttachedVehicleId { get; set; }
        public int VehicleFamilyId { get; set; }
        public int VehicleBrandId { get; set; }
        public int TireSizeId { get; set; }
        public int TiresCount { get; set; }
        public int ManufacturingYear { get; set; }
        public string Capacity { get; set; }
        public string ChassisType { get; set; }
        public string ChassisSerial { get; set; }
        public string EngineType { get; set; }
        public string EngineSerial { get; set; }
        public int CostCenterId { get; set; }
        public int VehicleStatusId { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDts { get; set; }
        public int VehicleOwnerId { get; set; }
        public int VehicleDepartmentId { get; set; }
        public bool Enable { get; set; }      
        [Required]
        public string SystemUserCreate { get; set; }
        [Required]
        public string LicenseNumber { get; set; }

        public virtual Vehicle AttachedVehicle { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public virtual TireSize TireSize { get; set; }
        public virtual VehicleBrand VehicleBrand { get; set; }
        public virtual VehicleDepartment VehicleDepartment { get; set; }
        public virtual VehicleFamily VehicleFamily { get; set; }
        public virtual VehicleOwner VehicleOwner { get; set; }
        public virtual VehicleStatus VehicleStatus { get; set; }
        public virtual Vehicle InverseAttachedVehicle { get; set; }
        public virtual ICollection<InventoryItemHistory> InventoryItemHistories { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<TireTest> TireTests { get; set; }
        public virtual ICollection<VehicleAttachment> VehicleAttachmentAttachedVehicles { get; set; }
        public virtual ICollection<VehicleAttachment> VehicleAttachmentVehicles { get; set; }
        public virtual ICollection<VehicleLicense> VehicleLicenses { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
