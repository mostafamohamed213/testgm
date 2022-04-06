using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemType
    {
        public InventoryItemType()
        {
            InventoryItemStatusInventoryItemTypes = new HashSet<InventoryItemStatusInventoryItemType>();
            InventoryItems = new HashSet<InventoryItem>();
        }

        public int InventoryItemTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InventoryItemTypeClassificationId { get; set; }
        public int? InventoryItemTypeUnitId { get; set; }
        public int? CostCenterId { get; set; }
        public bool IsQuantitative { get; set; }
        public bool AutoGenerateSerial { get; set; }
        public string PathImage { get; set; }
        public DateTime CreateDts { get; set; }
        public bool IsEnabled { get; set; }
        public int SystemUserId { get; set; }
        public int BrandId { get; set; }
        public int? ModelId { get; set; }
        public int? InventoryitemcategoryId { get; set; }
        public long WarehouseId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual InventoryItemTypeClassification InventoryItemTypeClassification { get; set; }
        public virtual InventoryItemTypeUnit InventoryItemTypeUnit { get; set; }
        public virtual InventoryItemCategory Inventoryitemcategory { get; set; }
        public virtual Model Model { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<InventoryItemStatusInventoryItemType> InventoryItemStatusInventoryItemTypes { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
