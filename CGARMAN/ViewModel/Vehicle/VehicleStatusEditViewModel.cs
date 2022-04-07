using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.Vehicle
{
    public class VehicleStatusEditViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "VehicleStatus", ErrorMessage = "this name already exist", AdditionalFields = "VehicleStatusId")]
        public string Name { get; set; }
        public int VehicleStatusId { get; set; }
        public string CreateDts { get; set; }
    }
}
