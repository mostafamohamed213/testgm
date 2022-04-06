using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.TechnicianViewModels
{
    public class DaysAttendancePagingViewModel
    {
        public List<Technician> items { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        public int itemsCount { get; set; }
        public int Tablelength { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public SelectList Companies { get; set; }
        public SelectList Positions { get; set; }
        public SelectList Status { get; set; }
        public SelectList Shifts { get; set; }
        public string Name { get; set; }
    }
}
