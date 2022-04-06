using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceItemInventoryItem
    {
        public long MaintenanceItemId { get; set; }
        public long InventoryItemId { get; set; }
        public decimal Quantity { get; set; }

        public virtual MaintenanceItem MaintenanceItem { get; set; }
    }
}
