using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.Vehicle
{
    public class VehicleOwnerEditViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "VehicleOwner", ErrorMessage = "this name already exist", AdditionalFields = "VehicleOwnerId")]
        public string Name { get; set; }
        public int VehicleOwnerId { get; set; }
        public string CreateDts { get; set; }
    }
}
