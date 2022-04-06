using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceItemType
    {
        public MaintenanceItemType()
        {
            MaintenanceItems = new HashSet<MaintenanceItem>();
        }

        public int MaintenanceItemTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MaintenanceItem> MaintenanceItems { get; set; }
    }
}
