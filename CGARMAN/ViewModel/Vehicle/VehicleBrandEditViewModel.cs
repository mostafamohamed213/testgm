using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.Vehicle
{
    public class VehicleBrandEditViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "VehicleBrand", ErrorMessage = "this name already exist in family", AdditionalFields = "VehicleBrandId,VehicleFamilyId")]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select family")]
        public int VehicleFamilyId { get; set; }      
        public int VehicleBrandId { get; set; }
        public string CreateDts { get; set; }
        public string FamilyName { get; set; }
        public SelectList Families { get; set; }
    }
}
