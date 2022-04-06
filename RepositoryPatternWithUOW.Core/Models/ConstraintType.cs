using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class ConstraintType
    {
        public ConstraintType()
        {
            ParameterConstraints = new HashSet<ParameterConstraint>();
            ParameterTypeConstraintTypes = new HashSet<ParameterTypeConstraintType>();
        }

        public int ConstraintTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ParameterConstraint> ParameterConstraints { get; set; }
        public virtual ICollection<ParameterTypeConstraintType> ParameterTypeConstraintTypes { get; set; }
    }
}
