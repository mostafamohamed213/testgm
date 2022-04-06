using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceType
    {
        public MaintenanceType()
        {
            Maintenances = new HashSet<Maintenance>();
            WorkshopMaintenanceTypes = new HashSet<WorkshopMaintenanceType>();
        }

        public int MaintenanceTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Maintenance> Maintenances { get; set; }
        public virtual ICollection<WorkshopMaintenanceType> WorkshopMaintenanceTypes { get; set; }
    }
}
