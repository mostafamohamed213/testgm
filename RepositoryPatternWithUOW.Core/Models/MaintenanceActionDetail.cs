using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceActionDetail
    {
        public MaintenanceActionDetail()
        {
            MaintenanceItems = new HashSet<MaintenanceItem>();
        }

        public int MaintenanceActionDetailId { get; set; }
        public int MaintenanceActionId { get; set; }
        public string Name { get; set; }

        public virtual MaintenanceAction MaintenanceAction { get; set; }
        public virtual ICollection<MaintenanceItem> MaintenanceItems { get; set; }
    }
}
