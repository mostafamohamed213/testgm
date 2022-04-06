using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.TechnicianViewModels
{
    public class CreateTechnicianViewModel
    {
        public int? TechnicianId { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Company")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select one!")]
        public int TechnicianCompanyId { get; set; }
        [Required(ErrorMessage = "this field is required.")]
        [Display(Name = "Company Employee ID")]
        public string TechnicianCompanyEmployeeId { get; set; }
        [Display(Name = "Position")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select one!")]
        public int TechnicianPositionId { get; set; }
        [Required]
        [Display(Name = "National ID")]
        //[StringLength(14,MinimumLength =14, ErrorMessage = "The national Id must be 14 characters.")]
        public string NationalId { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select one!")]
        public int CostCenterId { get; set; }        
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public bool Enable { get; set; }
    }
}
