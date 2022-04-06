using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Log
    {
        public Log()
        {
            LogDetails = new HashSet<LogDetail>();
        }

        public long LogId { get; set; }
        public DateTime LogDts { get; set; }
        public long SystemUserId { get; set; }
        public int LogCategoryId { get; set; }
        public int LogActionId { get; set; }
        public int LogStatusId { get; set; }

        public virtual LogAction LogAction { get; set; }
        public virtual LogCategory LogCategory { get; set; }
        public virtual LogStatus LogStatus { get; set; }
        public virtual SystemUser SystemUser { get; set; }
        public virtual ICollection<LogDetail> LogDetails { get; set; }
    }
}
