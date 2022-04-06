using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class VehicleBrand
    {
        public VehicleBrand()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int VehicleBrandId { get; set; }
        public string Name { get; set; }
        public int VehicleFamilyId { get; set; }

        public virtual VehicleFamily VehicleFamily { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
