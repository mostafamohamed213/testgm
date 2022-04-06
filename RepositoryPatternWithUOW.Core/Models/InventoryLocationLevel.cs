using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryLocationLevel
    {
        public InventoryLocationLevel()
        {
            InventoryLocations = new HashSet<InventoryLocation>();
            InverseParentInventoryLocationLevel = new HashSet<InventoryLocationLevel>();
            Warehouses = new HashSet<Warehouse>();
        }

        public int InventoryLocationLevelId { get; set; }
        public string Name { get; set; }
        public string StructureName { get; set; }
        public int? ParentInventoryLocationLevelId { get; set; }

        public virtual InventoryLocationLevel ParentInventoryLocationLevel { get; set; }
        public virtual ICollection<InventoryLocation> InventoryLocations { get; set; }
        public virtual ICollection<InventoryLocationLevel> InverseParentInventoryLocationLevel { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}
