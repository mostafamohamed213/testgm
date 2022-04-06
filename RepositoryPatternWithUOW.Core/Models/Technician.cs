using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Technician
    {
        public Technician()
        {
            MaintenanceItems = new HashSet<MaintenanceItem>();
            TechnicianAttendances = new HashSet<TechnicianAttendance>();
        }

        public int TechnicianId { get; set; }
        public string Name { get; set; }
        public int TechnicianCompanyId { get; set; }
        public string TechnicianCompanyEmployeeId { get; set; }
        public int TechnicianPositionId { get; set; }
        public string NationalId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CostCenterId { get; set; }
        public DateTime CreateDts { get; set; }
        public string ImagePath { get; set; }
        public bool Enable { get; set; }
        public string Systemusercrate { get; set; }

        public virtual CostCenter CostCenter { get; set; }
        public virtual TechnicianCompany TechnicianCompany { get; set; }
        public virtual TechnicianPosition TechnicianPosition { get; set; }
        public virtual ICollection<MaintenanceItem> MaintenanceItems { get; set; }
        public virtual ICollection<TechnicianAttendance> TechnicianAttendances { get; set; }
    }
}
