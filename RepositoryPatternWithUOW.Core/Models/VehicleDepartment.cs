using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class VehicleDepartment
    {
        public VehicleDepartment()
        {
            VehicleOwners = new HashSet<VehicleOwner>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int VehicleDepartmentId { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }
        public DateTime CreateDts { get; set; }
        [Required]
        public string SystemUserCreate { get; set; }

        public virtual ICollection<VehicleOwner> VehicleOwners { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
