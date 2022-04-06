using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class ParameterListValue
    {
        public long ParameterListValueId { get; set; }
        public int ParameterId { get; set; }
        public string Value { get; set; }
        public int? Rank { get; set; }

        public virtual Parameter Parameter { get; set; }
    }
}
