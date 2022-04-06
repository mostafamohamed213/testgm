using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class LogDetail
    {
        public long LogId { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }

        public virtual Log Log { get; set; }
    }
}
