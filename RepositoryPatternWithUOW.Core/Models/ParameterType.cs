using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class ParameterType
    {
        public ParameterType()
        {
            ParameterTypeConstraintTypes = new HashSet<ParameterTypeConstraintType>();
            Parameters = new HashSet<Parameter>();
        }

        public int ParameterTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ParameterTypeConstraintType> ParameterTypeConstraintTypes { get; set; }
        public virtual ICollection<Parameter> Parameters { get; set; }
    }
}
