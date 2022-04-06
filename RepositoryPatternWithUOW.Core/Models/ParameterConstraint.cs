using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class ParameterConstraint
    {
        public int ParameterId { get; set; }
        public int ConstraintTypeId { get; set; }
        public string Value { get; set; }

        public virtual ConstraintType ConstraintType { get; set; }
        public virtual Parameter Parameter { get; set; }
    }
}
