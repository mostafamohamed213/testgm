using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.TechnicianViewModels
{
    public class ViewTechnicianViewModel
    {
        public Technician technician { get; set; }
        public List<MaintenanceItem> maintenanceItems { get; set; }
        public List<TechnicianAttendance> TechnicianAttendances { get; set; }
       
    }
}
