using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    [Table("technician_attendance_status_log", Schema = "core")]
    public class TechnicianAttendanceStatusLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TechnicianAttendanceStatusLogId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Details { get; set; }
    }
}
