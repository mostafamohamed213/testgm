using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.TechnicianViewModels
{
    public class EditCompanyViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "TechnicianCompany", ErrorMessage = "this name already exist", AdditionalFields = "TechnicianCompanyId")]
        public string Name { get; set; }
        public int TechnicianCompanyId { get; set; }
        public string CreateDts { get; set; }
    }
}
