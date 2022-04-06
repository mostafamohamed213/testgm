using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class WarehousePermission
    {
        public long WarehouseId { get; set; }
        public long TargetWarehouseId { get; set; }

        public virtual Warehouse TargetWarehouse { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
