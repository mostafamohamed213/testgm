using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Parameter
    {
        public Parameter()
        {
            ParameterConstraints = new HashSet<ParameterConstraint>();
            ParameterEntities = new HashSet<ParameterEntity>();
            ParameterListValues = new HashSet<ParameterListValue>();
            ParameterValues = new HashSet<ParameterValue>();
        }

        public int ParameterId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int ParameterTypeId { get; set; }
        public bool IsOptional { get; set; }

        public virtual ParameterType ParameterType { get; set; }
        public virtual ICollection<ParameterConstraint> ParameterConstraints { get; set; }
        public virtual ICollection<ParameterEntity> ParameterEntities { get; set; }
        public virtual ICollection<ParameterListValue> ParameterListValues { get; set; }
        public virtual ICollection<ParameterValue> ParameterValues { get; set; }
    }
}
