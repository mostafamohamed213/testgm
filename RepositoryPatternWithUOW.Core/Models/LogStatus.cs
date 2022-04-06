using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class LogStatus
    {
        public LogStatus()
        {
            Logs = new HashSet<Log>();
        }

        public int LogStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
    }
}
