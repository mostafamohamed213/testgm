using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceActionControl
    {
        public int MaintenanceActionId { get; set; }
        public int VehicleFamilyId { get; set; }
        public bool Scheduled { get; set; }

        public virtual MaintenanceAction MaintenanceAction { get; set; }
        public virtual VehicleFamily VehicleFamily { get; set; }
    }
}
