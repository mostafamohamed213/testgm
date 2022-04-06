using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class ParameterTypeConstraintType
    {
        public int ParameterTypeId { get; set; }
        public int ConstraintTypeId { get; set; }

        public virtual ConstraintType ConstraintType { get; set; }
        public virtual ParameterType ParameterType { get; set; }
    }
}
