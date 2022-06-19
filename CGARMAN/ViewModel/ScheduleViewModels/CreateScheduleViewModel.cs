using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.ScheduleViewModels
{
    public class CreateScheduleViewModel
    {
       
        
        [Range(1, int.MaxValue, ErrorMessage = "Please select Plat Number")]
        public int PlatNumberId { get; set; }
        public SelectList PlatNumbers { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime FirstVisitDate { get; set; }
    }
}
