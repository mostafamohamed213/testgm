using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            InventoryItemTypes = new HashSet<InventoryItemType>();
            InventoryLocations = new HashSet<InventoryLocation>();
            WarehouseInventoryItemTypeClassifications = new HashSet<WarehouseInventoryItemTypeClassification>();
            WarehousePermissionTargetWarehouses = new HashSet<WarehousePermission>();
            WarehousePermissionWarehouses = new HashSet<WarehousePermission>();
        }

        public long WarehouseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? DivisionId { get; set; }
        public bool? IsEnabled { get; set; }
        public bool CanBroadcast { get; set; }
        public int? InventoryLocationLevelId { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual Division Division { get; set; }
        public virtual InventoryLocationLevel InventoryLocationLevel { get; set; }
        public virtual ICollection<InventoryItemType> InventoryItemTypes { get; set; }
        public virtual ICollection<InventoryLocation> InventoryLocations { get; set; }
        public virtual ICollection<WarehouseInventoryItemTypeClassification> WarehouseInventoryItemTypeClassifications { get; set; }
        public virtual ICollection<WarehousePermission> WarehousePermissionTargetWarehouses { get; set; }
        public virtual ICollection<WarehousePermission> WarehousePermissionWarehouses { get; set; }
    }
}
