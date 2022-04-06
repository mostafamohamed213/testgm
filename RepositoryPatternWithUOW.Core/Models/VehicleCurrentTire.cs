using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class VehicleCurrentTire
    {
        public long? VehicleId { get; set; }
        public long? InventoryItemId { get; set; }
        public int? TirePosition { get; set; }
        public string SerialNumber { get; set; }
        public string InventoryItemStatus { get; set; }
        public string Brand { get; set; }
        public string StandardTreadDepth { get; set; }
        public string Pattern { get; set; }
        public decimal? TireTreadDepthA { get; set; }
        public decimal? TireTreadDepthB { get; set; }
        public decimal? TireTreadDepthC { get; set; }
        public decimal? Pressure { get; set; }
        public DateTime? StartDts { get; set; }
    }
}
