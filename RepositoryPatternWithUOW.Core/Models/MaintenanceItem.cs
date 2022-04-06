using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceItem
    {
        public MaintenanceItem()
        {
            InventoryItemHistories = new HashSet<InventoryItemHistory>();
            InverseRelatedMaintenanceItem = new HashSet<MaintenanceItem>();
            MaintenanceItemInventoryItems = new HashSet<MaintenanceItemInventoryItem>();
            TireTests = new HashSet<TireTest>();
            VehicleTires = new HashSet<VehicleTire>();
        }

        public long MaintenanceItemId { get; set; }
        public long MaintenanceId { get; set; }
        public int MaintenanceItemTypeId { get; set; }
        public int MaintenanceItemStatusId { get; set; }
        public int TechnicianId { get; set; }
        public int? MaintenanceActionId { get; set; }
        public string Failure { get; set; }
        public string Comments { get; set; }
        public long? RelatedMaintenanceItemId { get; set; }
        public bool Checked { get; set; }
        public int? MaintenanceActionDetailId { get; set; }
        public string Task { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual Maintenance Maintenance { get; set; }
        public virtual MaintenanceAction MaintenanceAction { get; set; }
        public virtual MaintenanceActionDetail MaintenanceActionDetail { get; set; }
        public virtual MaintenanceItemStatus MaintenanceItemStatus { get; set; }
        public virtual MaintenanceItemType MaintenanceItemType { get; set; }
        public virtual MaintenanceItem RelatedMaintenanceItem { get; set; }
        public virtual Technician Technician { get; set; }
        public virtual ICollection<InventoryItemHistory> InventoryItemHistories { get; set; }
        public virtual ICollection<MaintenanceItem> InverseRelatedMaintenanceItem { get; set; }
        public virtual ICollection<MaintenanceItemInventoryItem> MaintenanceItemInventoryItems { get; set; }
        public virtual ICollection<TireTest> TireTests { get; set; }
        public virtual ICollection<VehicleTire> VehicleTires { get; set; }
    }
}
