using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.TechnicianViewModels
{
    public class CreateCompanyViewModel
    {
        [Required]       
        [Remote("ValidateIfNameExists", "TechnicianCompany", ErrorMessage = "this name already exist")]
        public string Name { get; set; }
    
    }
}
