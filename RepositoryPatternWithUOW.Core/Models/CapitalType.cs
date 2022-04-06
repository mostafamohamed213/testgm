using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class CapitalType
    {
        public CapitalType()
        {
            Divisions = new HashSet<Division>();
        }

        public int CapitalTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Division> Divisions { get; set; }
    }
}
