using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.Shared
{
    public class CostCenterEditViewModel
    {
        [Required]
        [Remote("ValidateIfNameExists", "CostCenter", ErrorMessage = "this name already exist", AdditionalFields = "CostCenterId")]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        public int CostCenterId { get; set; }
        public string CreateDts { get; set; }
    }
}
