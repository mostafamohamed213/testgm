using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class VehicleOwner
    {
        public VehicleOwner()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int VehicleOwnerId { get; set; }
        public string Name { get; set; }
        public int? VehicleDepartmentId { get; set; }

        public virtual VehicleDepartment VehicleDepartment { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
