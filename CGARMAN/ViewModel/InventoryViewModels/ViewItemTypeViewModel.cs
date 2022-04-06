using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.InventoryViewModels
{
    public class ViewItemTypeViewModel
    {
        public int InventoryItemTypeId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Classification { get; set; }
        public string CostCenter { get; set; }
        public int CostCenterId { get; set; }
        public bool AutoGenerateSerial { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Inventoryitemcategory { get; set; }
        public string Warehouse { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal CurrentQuantity { get; set; }
        public DateTime CreateDts { get; set; }
        public bool IsEnabled { get; set; }
        public string PathImage { get; set; }
        public ICollection<ViewItemViewModel> Items { get; set; }
        public List<InventoryItemStatus> Status { get; set; }

    }
    public class ViewItemViewModel
    {
        public long InventoryItemId { get; set; }
        public string SerialNumber { get; set; }
        public int VendorId { get; set; }
        public string Vendor { get; set; }
        public long LocationId { get; set; }       
        public long SystemUserId { get; set; }
        public DateTime CreateDts { get; set; }
        public int InventoryItemTypeId { get; set; }
        public int InventoryItemStatusId { get; set; }
        public string InventoryItemStatus { get; set; }
        public int? InventoryLocationId { get; set; }
        public string InventoryLocation { get; set; }       
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public int? CodeTypeId { get; set; }
        public string CodeType { get; set; }
        public string Code { get; set; }
        public string IssueNumber { get; set; }
        public string Notes { get; set; }
        public decimal CurrentQuantity { get; set; }
        public  Location Location { get; set; }

    }
}
