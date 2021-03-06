using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.IRepositories;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WMSContext _context;

        public IBaseRepository<Warehouse> Warehouse { get; private set; }
        public IBaseRepository<Brand> Brands { get; private set; }
        public IBaseRepository<Vendor> Vendors { get; private set; }
        public IBaseRepository<CodeType> CodeTypes { get; private set; }
        public IBaseRepository<InventoryItemTypeUnit> InventoryItemTypeUnits { get; private set; }
        public IBaseRepository<CostCenter> CostCenters { get; private set; }
        public IBaseRepository<InventoryItem> InventoryItems { get; private set; }
        public IBaseRepository<InventoryLocationLevel> InventoryLocationLevel { get; private set; }
        public IBaseRepository<InventoryLocation> InventoryLocation { get; private set; }
        public IBaseRepository<WarehouseInventoryItemTypeClassification> WarehouseInventoryItemTypeClassification { get; private set; }
        public IBaseRepository<InventoryItemType> InventoryItemType { get; private set; }
        public IBaseRepository<InventoryItemStatusInventoryItemType> InventoryItemStatusInventoryItemType { get; private set; }
        public IBaseRepository<InventoryItemStatus> InventoryItemStatus { get; private set; }
        public IBaseRepository<Model> Models { get; private set; }
        public IBaseRepository<ParameterEntity> ParameterEntities { get; private set; }
        public IBaseRepository<Parameter> Parameters { get; private set; }
        public IBaseRepository<ParameterListValue> ParameterListValues { get; private set; }
        public IBaseRepository<TireSize> TireSizes { get; private set; }
        public IBaseRepository<InventoryItemTypeClassification> InventoryItemTypeClassifications { get; private set; }
        public IBaseRepository<Location> Location { get; private set; }
        public IBaseRepository<ParameterValue> ParameterValue { get; private set; }
        public IBaseRepository<InventoryItemStatusLog> InventoryItemStatusLog { get; private set; }
        public IBaseRepository<InventoryTransactionDetail> InventoryTransactionDetail { get; private set; }
        public IBaseRepository<InventoryItemCategoryBrand> InventoryItemCategoryBrand { get; private set; }
        public IBaseRepository<InventoryLog> InventoryLog { get; }
        //Technician
        public IBaseRepository<Technician> Technician { get; }
        public IBaseRepository<TechnicianCompany> TechnicianCompany { get; }
        public IBaseRepository<TechnicianAttendance> TechnicianAttendance { get; }
        public IBaseRepository<TechnicianPosition> TechnicianPosition { get; }
        public IBaseRepository<Shift> Shift { get; }
        public IBaseRepository<AttendanceStatus> AttendanceStatus { get; }
        public IBaseRepository<MaintenanceActionTechnicianPosition> MaintenanceActionTechnicianPosition { get; }
        public IBaseRepository<MaintenanceItem> MaintenanceItem { get; }
        public IBaseRepository<TechnicianAttendanceLog> TechnicianAttendanceLog { get; }
        public IBaseRepository<TechnicianAttendanceStatusLog> TechnicianAttendanceStatusLog { get; }
        public IBaseRepository<VehicleStatus> VehicleStatus { get; }
        public IBaseRepository<VehicleDepartment> VehicleDepartment { get; }
        public IBaseRepository<VehicleOwner> VehicleOwner { get; }
        public IBaseRepository<VehicleFamily> VehicleFamily { get; }
        public IBaseRepository<VehicleBrand> VehicleBrand { get; }
        public IBaseRepository<Vehicle> Vehicle { get; }
        public IBaseRepository<VehicleLicense> VehicleLicense { get; }
        public IBaseRepository<VehicleAttachment> VehicleAttachment { get; }
        public IBaseRepository<VehicleCurrentTire> VehicleCurrentTire { get; }
        public IBaseRepository<InventoryItemAssignment> InventoryItemAssignment { get; }
        public IBaseRepository<VehicleTire> VehicleTire { get; }
        
        public IBaseRepository<Maintenance> Maintenance { get; }
        public IBaseRepository<WorkOrder> WorkOrder { get; }

        public IBaseRepository<Schedule> Schedule { get; }
        public UnitOfWork(WMSContext context)
        {
            _context = context;
            Warehouse = new BaseRepository<Warehouse>(_context);
            Brands = new BaseRepository<Brand>(_context);
            Vendors = new BaseRepository<Vendor>(_context);
            CodeTypes = new BaseRepository<CodeType>(_context);
            InventoryItemTypeUnits = new BaseRepository<InventoryItemTypeUnit>(_context);
            CostCenters = new BaseRepository<CostCenter>(_context);
            InventoryItems = new BaseRepository<InventoryItem>(_context);
            InventoryLocationLevel = new BaseRepository<InventoryLocationLevel>(_context);
            InventoryLocation = new BaseRepository<InventoryLocation>(_context);
            WarehouseInventoryItemTypeClassification = new BaseRepository<WarehouseInventoryItemTypeClassification>(_context);
            InventoryItemType = new BaseRepository<InventoryItemType>(_context);
            InventoryItemStatusInventoryItemType = new BaseRepository<InventoryItemStatusInventoryItemType>(_context);
            InventoryItemStatus = new BaseRepository<InventoryItemStatus>(_context);
            Models = new BaseRepository<Model>(_context);
            ParameterEntities = new BaseRepository<ParameterEntity>(_context);
            Parameters = new BaseRepository<Parameter>(_context);
            ParameterListValues = new BaseRepository<ParameterListValue>(_context);
            TireSizes = new BaseRepository<TireSize>(_context);
            InventoryItemTypeClassifications = new BaseRepository<InventoryItemTypeClassification>(_context);
            Location = new BaseRepository<Location>(_context);
            ParameterValue = new BaseRepository<ParameterValue>(_context);
            InventoryItemStatusLog = new BaseRepository<InventoryItemStatusLog>(_context);
            InventoryTransactionDetail = new BaseRepository<InventoryTransactionDetail> (_context);
            InventoryItemCategoryBrand = new BaseRepository<InventoryItemCategoryBrand>(_context);
            InventoryLog = new BaseRepository<InventoryLog>(_context);
            //Technician
            Technician = new BaseRepository<Technician>(_context);
            TechnicianCompany = new BaseRepository<TechnicianCompany>(_context);
            TechnicianAttendance = new BaseRepository<TechnicianAttendance>(_context);
            TechnicianPosition = new BaseRepository<TechnicianPosition>(_context);
            Shift = new BaseRepository<Shift>(_context);
            AttendanceStatus = new BaseRepository<AttendanceStatus>(_context);
            MaintenanceActionTechnicianPosition = new BaseRepository<MaintenanceActionTechnicianPosition>(_context);

            MaintenanceItem = new BaseRepository<MaintenanceItem>(_context);
            TechnicianAttendanceStatusLog = new BaseRepository<TechnicianAttendanceStatusLog>(_context);
            TechnicianAttendanceLog = new BaseRepository<TechnicianAttendanceLog>(_context);
            VehicleStatus = new BaseRepository<VehicleStatus>(_context);
            VehicleDepartment = new BaseRepository<VehicleDepartment>(_context);
            VehicleOwner = new BaseRepository<VehicleOwner>(_context);
            VehicleFamily = new BaseRepository<VehicleFamily>(_context);
            VehicleBrand = new BaseRepository<VehicleBrand>(_context);
            Vehicle = new BaseRepository<Vehicle>(_context);
            VehicleLicense = new BaseRepository<VehicleLicense>(_context);
            VehicleAttachment = new BaseRepository<VehicleAttachment>(_context);
            VehicleCurrentTire = new BaseRepository<VehicleCurrentTire>(_context);
            InventoryItemAssignment = new BaseRepository<InventoryItemAssignment>(_context);
            VehicleTire = new BaseRepository<VehicleTire>(_context);

            Maintenance = new BaseRepository<Maintenance>(_context);
            WorkOrder = new BaseRepository<WorkOrder>(_context);
            Schedule = new BaseRepository<Schedule>(_context);

        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
