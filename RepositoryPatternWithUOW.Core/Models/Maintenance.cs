using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Maintenance
    {
        public Maintenance()
        {
            MaintenanceItems = new HashSet<MaintenanceItem>();
            MaintenanceQuestionValues = new HashSet<MaintenanceQuestionValue>();
        }

        public long MaintenanceId { get; set; }
        public int MaintenanceTypeId { get; set; }
        public long WorkOrderNumber { get; set; }
        public string Notes { get; set; }
        public int WorkshopId { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual MaintenanceType MaintenanceType { get; set; }
        public virtual WorkOrder WorkOrderNumberNavigation { get; set; }
        public virtual Workshop Workshop { get; set; }
        public virtual ICollection<MaintenanceItem> MaintenanceItems { get; set; }
        public virtual ICollection<MaintenanceQuestionValue> MaintenanceQuestionValues { get; set; }
    }
}
