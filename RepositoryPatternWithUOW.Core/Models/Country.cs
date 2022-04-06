using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Country
    {
        public Country()
        {
            Divisions = new HashSet<Division>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<Division> Divisions { get; set; }
    }
}
