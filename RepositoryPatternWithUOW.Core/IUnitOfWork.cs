using RepositoryPatternWithUOW.Core.IRepositories;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Warehouse> Warehouse { get; }
        IBaseRepository<Brand> Brands { get; }
        IBaseRepository<Vendor> Vendors { get; }
        IBaseRepository<CodeType> CodeTypes { get; }
        IBaseRepository<InventoryItemTypeUnit> InventoryItemTypeUnits { get; }
        IBaseRepository<CostCenter> CostCenters { get; }
        IBaseRepository<InventoryItem> InventoryItems { get; }
        IBaseRepository<InventoryLocationLevel> InventoryLocationLevel { get; }
        IBaseRepository<InventoryLocation> InventoryLocation { get; }
        IBaseRepository<WarehouseInventoryItemTypeClassification> WarehouseInventoryItemTypeClassification { get; }
        IBaseRepository<InventoryItemType> InventoryItemType { get; }
        IBaseRepository<InventoryItemStatusInventoryItemType> InventoryItemStatusInventoryItemType { get; }
        IBaseRepository<InventoryItemStatus> InventoryItemStatus { get; }
        IBaseRepository<Model> Models { get; }
        IBaseRepository<ParameterEntity> ParameterEntities { get; }
        IBaseRepository<Parameter> Parameters { get; }
        IBaseRepository<ParameterListValue> ParameterListValues { get; }
        IBaseRepository<TireSize> TireSizes { get; }
        IBaseRepository<InventoryItemTypeClassification> InventoryItemTypeClassifications { get; }
        IBaseRepository<Location> Location { get; }
        IBaseRepository<ParameterValue> ParameterValue { get; }
        IBaseRepository<InventoryItemStatusLog> InventoryItemStatusLog { get; }
        IBaseRepository<InventoryTransactionDetail> InventoryTransactionDetail { get; }
        IBaseRepository<InventoryItemCategoryBrand> InventoryItemCategoryBrand { get; }
        IBaseRepository<InventoryLog> InventoryLog { get; }
        //Technician
        IBaseRepository<Technician> Technician { get; }
        IBaseRepository<TechnicianCompany> TechnicianCompany { get; }
        IBaseRepository<TechnicianAttendance> TechnicianAttendance { get; }
        IBaseRepository<TechnicianPosition> TechnicianPosition { get; }
        IBaseRepository<Shift> Shift { get; }
        IBaseRepository<AttendanceStatus> AttendanceStatus { get; }
        IBaseRepository<MaintenanceActionTechnicianPosition> MaintenanceActionTechnicianPosition { get; }
        IBaseRepository<MaintenanceItem> MaintenanceItem { get; }
        IBaseRepository<TechnicianAttendanceLog> TechnicianAttendanceLog { get; }
        IBaseRepository<TechnicianAttendanceStatusLog> TechnicianAttendanceStatusLog { get; }
        int Complete();
    }
}
