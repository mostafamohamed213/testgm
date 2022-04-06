using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class VehicleTire
    {
        public long VehicleId { get; set; }
        public long InventoryItemId { get; set; }
        public DateTime StartDts { get; set; }
        public DateTime? EndDts { get; set; }
        public int TirePosition { get; set; }
        public decimal TireTreadDepthA { get; set; }
        public decimal TireTreadDepthB { get; set; }
        public decimal TireTreadDepthC { get; set; }
        public decimal Pressure { get; set; }
        public DateTime CreateDts { get; set; }
        public long? MaintenanceItemId { get; set; }

        public virtual MaintenanceItem MaintenanceItem { get; set; }
    }
}
