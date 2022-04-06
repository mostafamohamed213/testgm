using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class LogAction
    {
        public LogAction()
        {
            Logs = new HashSet<Log>();
        }

        public int LogActionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
    }
}
