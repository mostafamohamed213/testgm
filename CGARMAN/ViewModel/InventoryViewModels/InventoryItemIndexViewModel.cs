using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.InventoryViewModels
{
    public class InventoryItemIndexViewModel
    {
        public int InventoryItemTypeId { get; set; }
        public long InventoryItemId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Supplier { get; set; }
        public string SerialNumber{get; set;}
        public string Status { get; set; }
        public string Brand { get; set; }

    }
    public class InventoryItemTypeIndexViewModel
    {
        public int InventoryItemTypeId { get; set; }        
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Classification { get; set; }
        public string CostCenter { get; set; }      
        public bool AutoGenerateSerial { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Inventoryitemcategory { get; set; }
        public string Warehouse { get; set; }

    }
}
