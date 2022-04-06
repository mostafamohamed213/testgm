using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class TechnicianPosition
    {
        public TechnicianPosition()
        {
            MaintenanceActionTechnicianPositions = new HashSet<MaintenanceActionTechnicianPosition>();
            Technicians = new HashSet<Technician>();
        }

        public int TechnicianPositionId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDts { get; set; }
        public bool Enable { get; set; }
        [Required]
        public string SystemUserID { get; set; }
        public virtual ICollection<MaintenanceActionTechnicianPosition> MaintenanceActionTechnicianPositions { get; set; }
        public virtual ICollection<Technician> Technicians { get; set; }
    }
}
