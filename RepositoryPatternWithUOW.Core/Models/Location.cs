using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Location
    {
        public Location()
        {
            InventoryItems = new HashSet<InventoryItem>();
            InventoryTransactionFromLocations = new HashSet<InventoryTransaction>();
            InventoryTransactionToLocations = new HashSet<InventoryTransaction>();
        }

        public long LocationId { get; set; }
        public long LocationObjectId { get; set; }
        public int LocationTypeId { get; set; }

        public virtual LocationType LocationType { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
        public virtual ICollection<InventoryTransaction> InventoryTransactionFromLocations { get; set; }
        public virtual ICollection<InventoryTransaction> InventoryTransactionToLocations { get; set; }
    }
}
