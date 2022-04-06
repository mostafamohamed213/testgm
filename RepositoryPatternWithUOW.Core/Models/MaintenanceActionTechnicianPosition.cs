using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceActionTechnicianPosition
    {
        public int MaintenanceActionId { get; set; }
        public int TechnicianPositionId { get; set; }

        public virtual MaintenanceAction MaintenanceAction { get; set; }
        public virtual TechnicianPosition TechnicianPosition { get; set; }
    }
}
