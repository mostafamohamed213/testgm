using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class ParameterValue
    {
        public int ParameterValueId { get; set; }
        public long ObjectId { get; set; }
        public int ParameterId { get; set; }
        public string Value { get; set; }

        public virtual Parameter Parameter { get; set; }
    }
}
