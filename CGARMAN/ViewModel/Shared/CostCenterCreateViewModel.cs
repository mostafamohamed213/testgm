using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.Shared
{
    public class CostCenterCreateViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "CostCenter", ErrorMessage = "this name already exist")]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
