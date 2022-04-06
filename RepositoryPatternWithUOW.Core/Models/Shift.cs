using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Shift
    {
        public Shift()
        {
            TechnicianAttendances = new HashSet<TechnicianAttendance>();
        }

        public int ShiftId { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }

        public virtual ICollection<TechnicianAttendance> TechnicianAttendances { get; set; }
    }
}
