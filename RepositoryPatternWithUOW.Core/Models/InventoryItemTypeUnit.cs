using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemTypeUnit
    {
        public InventoryItemTypeUnit()
        {
            InventoryItemTypes = new HashSet<InventoryItemType>();
        }

        public int InventoryItemTypeUnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<InventoryItemType> InventoryItemTypes { get; set; }
    }
}
