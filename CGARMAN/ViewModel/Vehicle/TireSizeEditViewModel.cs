using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.Vehicle
{
    public class TireSizeEditViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "TireSize", ErrorMessage = "this name already exist", AdditionalFields = "TireSizeId")]
        public string Name { get; set; }
        public int TireSizeId { get; set; }
        public string CreateDts { get; set; }
    }
}
