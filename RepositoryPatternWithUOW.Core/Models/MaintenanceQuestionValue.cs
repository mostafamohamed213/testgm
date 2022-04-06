using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class MaintenanceQuestionValue
    {
        public int MaintenanceQuestionId { get; set; }
        public long MaintenanceId { get; set; }
        public string Value { get; set; }

        public virtual Maintenance Maintenance { get; set; }
        public virtual MaintenanceQuestion MaintenanceQuestion { get; set; }
    }
}
