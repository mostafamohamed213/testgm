using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class VehicleFamily
    {
        public VehicleFamily()
        {
            InverseParentVehicleFamily = new HashSet<VehicleFamily>();
            MaintenanceActionControls = new HashSet<MaintenanceActionControl>();
            VehicleBrands = new HashSet<VehicleBrand>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int VehicleFamilyId { get; set; }
        public string Name { get; set; }
        public int? ParentVehicleFamilyId { get; set; }
        public bool Enable { get; set; }
        public DateTime CreateDts { get; set; }
        [Required]
        public string SystemUserCreate { get; set; }

        public virtual VehicleFamily ParentVehicleFamily { get; set; }
        public virtual ICollection<VehicleFamily> InverseParentVehicleFamily { get; set; }
        public virtual ICollection<MaintenanceActionControl> MaintenanceActionControls { get; set; }
        public virtual ICollection<VehicleBrand> VehicleBrands { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
