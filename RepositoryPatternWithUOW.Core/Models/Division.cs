using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Division
    {
        public Division()
        {
            InverseParentDivision = new HashSet<Division>();
            Warehouses = new HashSet<Warehouse>();
        }

        public long DivisionId { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public long? ParentDivisionId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? CapitalTypeId { get; set; }

        public virtual CapitalType CapitalType { get; set; }
        public virtual Country Country { get; set; }
        public virtual Division ParentDivision { get; set; }
        public virtual ICollection<Division> InverseParentDivision { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}
