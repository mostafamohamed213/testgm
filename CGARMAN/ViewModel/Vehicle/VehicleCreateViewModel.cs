using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.Vehicle
{
    public class VehicleCreateViewModel
    {
        public long? VehicleId { get; set; }
        public int VehicleFamilyId { get; set; }
        public int VehicleBrandId { get; set; }
        public int TireSizeId { get; set; }
        [Range(4, int.MaxValue, ErrorMessage = "Please enter a valid count for tires")]
        public int TiresCount { get; set; }
        public int ManufacturingYear { get; set; }
       
        public string Capacity { get; set; }
       
        public string ChassisType { get; set; }
     
        public string ChassisSerial { get; set; }
     
        public string EngineType { get; set; }
       
        public string EngineSerial { get; set; }
        public int CostCenterId { get; set; }
        public int VehicleStatusId { get; set; }
        public string Notes { get; set; }       
        public int VehicleOwnerId { get; set; }
        public int VehicleDepartmentId { get; set; }
        [Required]
        [Remote("ValidateIfPlateNumberExists", "Vehicle", ErrorMessage = "this Plate Number already exist", AdditionalFields = "VehicleId")]
        public string LicenseNumber { get; set; }

        //drop down menus
        public SelectList Departments { get; set; }
        public SelectList Owners { get; set; }
        public SelectList Families { get; set; }
        public SelectList TireSizes { get; set; }
        public SelectList CostCenters { get; set; }
        public SelectList Status { get; set; }
      //  public SelectList ManufacturingYears { get; set; }
        

    }
}
