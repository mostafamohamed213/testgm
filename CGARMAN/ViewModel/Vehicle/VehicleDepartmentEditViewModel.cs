using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.Vehicle
{
    public class VehicleDepartmentEditViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "VehicleDepartment", ErrorMessage = "this name already exist", AdditionalFields = "VehicleDepartmentId")]
        public string Name { get; set; }
        public int VehicleDepartmentId { get; set; }
        public string CreateDts { get; set; }

    }
}
