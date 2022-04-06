using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemLevel
    {
        public long? LocationId { get; set; }
        public int? InventoryItemTypeId { get; set; }
        public int? InventoryLocationId { get; set; }
        public decimal? Quantity { get; set; }
    }
}
