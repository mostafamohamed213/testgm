using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Consts
{
    public static class TablesMaxRows
    {
    
        public static int InventoryItemIndex  { get; set; }
        public static int InventoryItemTypeIndex { get; set; }
        public static int InventoryStatusIndex { get; set; }
        public static int InventoryUnitsIndex { get; set; }
        public static int VendorIndex { get; set; }        
        public static int InventoryClassificationIndex { get; set; }
        public static int InventoryBrandIndex { get; set; }
        public static int InventoryModelIndex { get; set; }
        public static int TechnicianIndex { get; set; }
        public static int CompaniesIndex { get; set; }
        public static int ShiftIndex { get; set; }
        public static int PositionIndex { get; set; }
        public static int CostCenterIndex { get; set; }
        public static int AttendanceIndex { get; set; }
        public static int AttendanceDaysIndex { get; set; }
        public static int TireSizeIndex { get; set; }
        public static int VehicleStatusIndex { get; set; }
        public static int VehicleDepartmentIndex { get; set; }
        public static int VehicleOwnerIndex { get; set; }
        public static int VehicleFamilyIndex { get; set; }
        public static int VehicleBrandIndex { get; set; }
        public static int VehicleIndex { get; set; }      


        static TablesMaxRows()
        {
            InventoryItemIndex = 10;
            InventoryItemTypeIndex = 10;
            InventoryStatusIndex = 10;
            InventoryUnitsIndex = 10;
            VendorIndex = 10;
            InventoryClassificationIndex = 10;
            InventoryBrandIndex = 10;
            InventoryModelIndex = 10;
            TechnicianIndex = 10;
            CompaniesIndex = 10;
            ShiftIndex = 10;
            PositionIndex = 10;
            CostCenterIndex = 10;
            AttendanceIndex = 10;
            AttendanceDaysIndex = 10;
            TireSizeIndex = 10;
            VehicleStatusIndex = 10;
            VehicleDepartmentIndex = 10;
            VehicleOwnerIndex = 10;
            VehicleFamilyIndex = 10;
            VehicleBrandIndex = 10;
            VehicleIndex = 10;
        }
    }
}
