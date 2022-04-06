using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceItemStatus
    {
        public MaintenanceItemStatus()
        {
            MaintenanceItems = new HashSet<MaintenanceItem>();
        }

        public int MaintenanceItemStatusId { get; set; }
        public string Name { get; set; }
        public int WorkshopId { get; set; }

        public virtual Workshop Workshop { get; set; }
        public virtual ICollection<MaintenanceItem> MaintenanceItems { get; set; }
    }
}
