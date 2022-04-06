using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class ParameterEntity
    {
        public int ParameterId { get; set; }
        public int EntityId { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual Parameter Parameter { get; set; }
    }
}
