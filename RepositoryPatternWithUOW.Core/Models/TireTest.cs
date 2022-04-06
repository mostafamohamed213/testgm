using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class TireTest
    {
        public long TireTestId { get; set; }
        public long InventoryItemId { get; set; }
        public long VehicleId { get; set; }
        public long MaintenanceItemId { get; set; }
        public decimal TireTreadDepthA { get; set; }
        public decimal TireTreadDepthB { get; set; }
        public decimal TireTreadDepthC { get; set; }
        public int TireConditionId { get; set; }
        public decimal Psi { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual MaintenanceItem MaintenanceItem { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
