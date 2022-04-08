using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.Vehicle
{
    public class VehicleFamilyEditViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "VehicleFamily", ErrorMessage = "this name already exist", AdditionalFields = "VehicleFamilyId")]
        public string Name { get; set; }
        public int VehicleFamilyId { get; set; }
        public string CreateDts { get; set; }
    }
}
