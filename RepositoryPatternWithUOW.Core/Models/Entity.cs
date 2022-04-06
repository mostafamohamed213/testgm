using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Entity
    {
        public Entity()
        {
            ParameterEntities = new HashSet<ParameterEntity>();
        }

        public int EntityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ParameterEntity> ParameterEntities { get; set; }
    }
}
