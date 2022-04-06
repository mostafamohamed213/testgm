using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Workshop
    {
        public Workshop()
        {
            MaintenanceActions = new HashSet<MaintenanceAction>();
            MaintenanceItemStatuses = new HashSet<MaintenanceItemStatus>();
            MaintenanceQuestions = new HashSet<MaintenanceQuestion>();
            Maintenances = new HashSet<Maintenance>();
            WorkOrderWorkshops = new HashSet<WorkOrderWorkshop>();
            WorkshopMaintenanceTypes = new HashSet<WorkshopMaintenanceType>();
            WorkshopWarehouses = new HashSet<WorkshopWarehouse>();
        }

        public int WorkshopId { get; set; }
        public string Name { get; set; }
        public string Library { get; set; }

        public virtual ICollection<MaintenanceAction> MaintenanceActions { get; set; }
        public virtual ICollection<MaintenanceItemStatus> MaintenanceItemStatuses { get; set; }
        public virtual ICollection<MaintenanceQuestion> MaintenanceQuestions { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }
        public virtual ICollection<WorkOrderWorkshop> WorkOrderWorkshops { get; set; }
        public virtual ICollection<WorkshopMaintenanceType> WorkshopMaintenanceTypes { get; set; }
        public virtual ICollection<WorkshopWarehouse> WorkshopWarehouses { get; set; }
    }
}
