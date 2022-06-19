using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            WorkOrders = new HashSet<WorkOrder>();
        }

        public long ScheduleId { get; set; }
        public long VehicleId { get; set; }
        public DateTime VisitDts { get; set; }
        public string CreateSystemUserId { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public bool Enable { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
