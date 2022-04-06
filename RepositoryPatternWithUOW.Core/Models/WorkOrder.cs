using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class WorkOrder
    {
        public WorkOrder()
        {
            Maintenances = new HashSet<Maintenance>();
            WorkOrderWorkshops = new HashSet<WorkOrderWorkshop>();
        }

        public long WorkOrderNumber { get; set; }
        public long? ScheduleId { get; set; }
        public long VehicleId { get; set; }
        public bool IsAccident { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Mileage { get; set; }
        public int MaintenanceFleetId { get; set; }
        public long SystemUserId { get; set; }
        public DateTime CreateDts { get; set; }
        public bool IsFinished { get; set; }

        public virtual MaintenanceFleet MaintenanceFleet { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }
        public virtual ICollection<WorkOrderWorkshop> WorkOrderWorkshops { get; set; }
    }
}
