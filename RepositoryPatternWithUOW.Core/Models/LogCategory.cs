using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class LogCategory
    {
        public LogCategory()
        {
            InverseParentLogCategory = new HashSet<LogCategory>();
            Logs = new HashSet<Log>();
        }

        public int LogCategoryId { get; set; }
        public string Name { get; set; }
        public int? ParentLogCategoryId { get; set; }

        public virtual LogCategory ParentLogCategory { get; set; }
        public virtual ICollection<LogCategory> InverseParentLogCategory { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
