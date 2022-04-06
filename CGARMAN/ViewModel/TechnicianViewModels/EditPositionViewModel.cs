using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.TechnicianViewModels
{
    public class EditPositionViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "TechnicianPosition", ErrorMessage = "this name already exist", AdditionalFields = "TechnicianPositionId")]
        public string Name { get; set; }
        public int TechnicianPositionId { get; set; }
        public string CreateDts { get; set; }
    }
}
