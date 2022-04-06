using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.TechnicianViewModels
{
    public class EditShiftViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "TechnicianShift", ErrorMessage = "this name already exist", AdditionalFields = "TechnicianShiftId")]
        public string Name { get; set; }
        public int TechnicianShiftId { get; set; }      
    }
}
