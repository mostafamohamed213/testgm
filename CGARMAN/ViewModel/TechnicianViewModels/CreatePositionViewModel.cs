using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.TechnicianViewModels
{
    public class CreatePositionViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "TechnicianPosition", ErrorMessage = "this name already exist")]
        public string Name { get; set; }
    }
}
