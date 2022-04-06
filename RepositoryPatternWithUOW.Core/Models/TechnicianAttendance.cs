using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class TechnicianAttendance
    {
        public int TechnicianId { get; set; }
        public DateTime EventDate { get; set; }
        public int ShiftId { get; set; }
        public int AttendanceStatusId { get; set; }
        public TimeSpan? InTime { get; set; }
        public TimeSpan? OutTime { get; set; }
        public DateTime CreateDts { get; set; }
        [Required]
        public string Systemusercrate { get; set; }
        public virtual AttendanceStatus AttendanceStatus { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual Technician Technician { get; set; }
    }
}
