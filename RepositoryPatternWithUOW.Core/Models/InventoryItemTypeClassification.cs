using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemTypeClassification
    {
        public InventoryItemTypeClassification()
        {
            InventoryItemTypes = new HashSet<InventoryItemType>();
            WarehouseInventoryItemTypeClassifications = new HashSet<WarehouseInventoryItemTypeClassification>();
        }

        public int InventoryItemTypeClassificationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<InventoryItemType> InventoryItemTypes { get; set; }
        public virtual ICollection<WarehouseInventoryItemTypeClassification> WarehouseInventoryItemTypeClassifications { get; set; }
    }
}
