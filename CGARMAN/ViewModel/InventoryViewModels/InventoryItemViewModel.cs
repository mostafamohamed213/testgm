using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.InventoryViewModels
{
    public class InventoryItemViewModel
    {
        public long InventoryItemId { get; set; }
        [Required]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Display(Name = "Supplier")]
        public int VendorId { get; set; }
        [Display(Name = "Warehouse")]
        public int WarehouseId { get; set; }
        [Display(Name = "Location")]
        public long LocationId { get; set; }
        [Display(Name = "Type")]
        public int InventoryItemTypeId { get; set; }
        [Display(Name = "Status")]
        public int InventoryItemStatusId { get; set; }
        [Display(Name = "Category")]
        public int InventoryItemCategoryId { get; set; }
        [Display(Name = "Location")]
        public int? InventoryLocationId { get; set; }
        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        [Display(Name = "Model")]
        public int? ModelId { get; set; }
        public decimal Quantity { get; set; }
        [Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }
        [Display(Name = "Code Type")]
        public int? CodeTypeId { get; set; }
        public string Code { get; set; }
        [Display(Name = "Issue Number")]
        public string IssueNumber { get; set; }
        public string Notes { get; set; }
        public SelectList Warehouses { get; set; }
        public SelectList Suppliers { get; set; }        
        public SelectList Brands { get; set; }
        public SelectList CodeType { get; set; }

    }
}
