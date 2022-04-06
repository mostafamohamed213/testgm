using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class VehicleStatus
    {
        public VehicleStatus()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int VehicleStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
