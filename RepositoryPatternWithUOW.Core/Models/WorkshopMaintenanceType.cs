using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class WorkshopMaintenanceType
    {
        public int WorkshopId { get; set; }
        public int MaintenanceTypeId { get; set; }

        public virtual MaintenanceType MaintenanceType { get; set; }
        public virtual Workshop Workshop { get; set; }
    }
}
