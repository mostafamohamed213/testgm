using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            InventoryItems = new HashSet<InventoryItem>();
        }

        public int VendorId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
