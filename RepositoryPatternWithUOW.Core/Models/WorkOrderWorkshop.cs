using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class WorkOrderWorkshop
    {
        public long WorkOrderNumber { get; set; }
        public int WorkshopId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? TotalFinding { get; set; }
        public int? Pending { get; set; }
        public int? Done { get; set; }

        public virtual WorkOrder WorkOrderNumberNavigation { get; set; }
        public virtual Workshop Workshop { get; set; }
    }
}
