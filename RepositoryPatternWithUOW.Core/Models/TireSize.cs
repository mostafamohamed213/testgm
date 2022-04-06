using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class TireSize
    {
        public TireSize()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int TireSizeId { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
