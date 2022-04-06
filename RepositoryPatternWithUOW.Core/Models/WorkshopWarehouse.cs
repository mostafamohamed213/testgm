using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class WorkshopWarehouse
    {
        public int WorkshopId { get; set; }
        public long WarehouseId { get; set; }
        public bool EarlySelect { get; set; }
        public string LibraryPath { get; set; }

        public virtual Workshop Workshop { get; set; }
    }
}
