using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    [Table("technician_attendance_log", Schema = "core")]

    public class TechnicianAttendanceLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long TechnicianAttendanceLogId { get; set; }
        public string Object { get; set; }
        public string Details { get; set; }

        public DateTime CreateDts { get; set; }
        [Required]
        public string Systemusercrate { get; set; }
        public int TechnicianId { get; set; }
        public DateTime EventDate { get; set; }
        public int AttendanceStatusId { get; set; }
        [ForeignKey("AttendanceStatusId")]
        public virtual AttendanceStatus AttendanceStatus { get; set; }        
        public int ShiftId { get; set; }
        [ForeignKey("ShiftId")]
        public virtual Shift Shift { get; set; }       
        public int TechnicianAttendanceStatusLogId { get; set; }
        [ForeignKey("TechnicianAttendanceStatusLogId")]
        public virtual TechnicianAttendanceStatusLog TechnicianAttendanceStatusLog { get; set; }

    }
}
