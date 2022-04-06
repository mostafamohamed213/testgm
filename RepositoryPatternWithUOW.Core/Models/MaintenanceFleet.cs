using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceFleet
    {
        public MaintenanceFleet()
        {
            WorkOrders = new HashSet<WorkOrder>();
        }

        public int MaintenanceFleetId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
