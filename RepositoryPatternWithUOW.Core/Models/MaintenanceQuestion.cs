using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceQuestion
    {
        public MaintenanceQuestion()
        {
            MaintenanceQuestionValues = new HashSet<MaintenanceQuestionValue>();
        }

        public int MaintenanceQuestionId { get; set; }
        public string Name { get; set; }
        public int WorkshopId { get; set; }

        public virtual Workshop Workshop { get; set; }
        public virtual ICollection<MaintenanceQuestionValue> MaintenanceQuestionValues { get; set; }
    }
}
