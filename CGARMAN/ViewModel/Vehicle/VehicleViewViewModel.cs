using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.Vehicle
{
    public class VehicleViewViewModel
    {
        public long VehicleId { get; set; }     
        public int TiresCount { get; set; }
        public int ManufacturingYear { get; set; }
        public string Capacity { get; set; }
        public string ChassisType { get; set; }
        public string ChassisSerial { get; set; }
        public string EngineType { get; set; }
        public string EngineSerial { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDts { get; set; }       
        public string LicenseNumber { get; set; }
        public RepositoryPatternWithUOW.Core.Models.Vehicle AttachedVehicle { get; set; }
        public CostCenter CostCenter { get; set; }
        public TireSize TireSize { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public VehicleDepartment VehicleDepartment { get; set; }
        public VehicleFamily VehicleFamily { get; set; }
        public VehicleOwner VehicleOwner { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public virtual ICollection<VehicleLicense> VehicleLicenses { get; set; }
        public bool isVehicle { get; set; }
        public List<VehicleCurrentTire> vehicleCurrentTire { get; set; }
    }
}
