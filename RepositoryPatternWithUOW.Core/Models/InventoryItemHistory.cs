using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemHistory
    {
        public long InventoryItemId { get; set; }
        public int WarehouseId { get; set; }
        public long VehicleId { get; set; }
        public long MaintenanceItemId { get; set; }
        public DateTime StartDts { get; set; }
        public DateTime? EndDts { get; set; }

        public virtual MaintenanceItem MaintenanceItem { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
