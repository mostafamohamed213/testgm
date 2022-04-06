using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryLocationStructure
    {
        public int? InventoryLocationId { get; set; }
        public string Name { get; set; }
        public long? WarehouseId { get; set; }
        public int? InventoryLocationLevelId { get; set; }
    }
}
