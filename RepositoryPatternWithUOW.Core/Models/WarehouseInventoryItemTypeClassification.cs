using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class WarehouseInventoryItemTypeClassification
    {
        public long WarehouseId { get; set; }
        public int InventoryItemTypeClassificationId { get; set; }

        public virtual InventoryItemTypeClassification InventoryItemTypeClassification { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
