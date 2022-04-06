using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryLocation
    {
        public InventoryLocation()
        {
            InventoryItems = new HashSet<InventoryItem>();
            InverseParentInventoryLocation = new HashSet<InventoryLocation>();
        }

        public int InventoryLocationId { get; set; }
        public string Name { get; set; }
        public long WarehouseId { get; set; }
        public int InventoryLocationLevelId { get; set; }
        public int? ParentInventoryLocationId { get; set; }

        public virtual InventoryLocationLevel InventoryLocationLevel { get; set; }
        public virtual InventoryLocation ParentInventoryLocation { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
        public virtual ICollection<InventoryLocation> InverseParentInventoryLocation { get; set; }
    }
}
