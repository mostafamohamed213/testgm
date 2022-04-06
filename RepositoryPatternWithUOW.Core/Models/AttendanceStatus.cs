using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class AttendanceStatus
    {
        public AttendanceStatus()
        {
            TechnicianAttendances = new HashSet<TechnicianAttendance>();
        }

        public int AttendanceStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TechnicianAttendance> TechnicianAttendances { get; set; }
    }
}
