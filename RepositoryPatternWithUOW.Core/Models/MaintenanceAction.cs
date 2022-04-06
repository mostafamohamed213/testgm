using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceAction
    {
        public MaintenanceAction()
        {
            MaintenanceActionControls = new HashSet<MaintenanceActionControl>();
            MaintenanceActionDetails = new HashSet<MaintenanceActionDetail>();
            MaintenanceActionTechnicianPositions = new HashSet<MaintenanceActionTechnicianPosition>();
            MaintenanceItems = new HashSet<MaintenanceItem>();
        }

        public int MaintenanceActionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int WorkshopId { get; set; }

        public virtual Workshop Workshop { get; set; }
        public virtual ICollection<MaintenanceActionControl> MaintenanceActionControls { get; set; }
        public virtual ICollection<MaintenanceActionDetail> MaintenanceActionDetails { get; set; }
        public virtual ICollection<MaintenanceActionTechnicianPosition> MaintenanceActionTechnicianPositions { get; set; }
        public virtual ICollection<MaintenanceItem> MaintenanceItems { get; set; }
    }
}
