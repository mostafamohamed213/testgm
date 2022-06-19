using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.InventoryViewModels
{
    public class ItemTypeViewModel
    {
        public int InventoryItemTypeId { get; set; }        
        public string InventoryItemTypeName { get; set; }
        public bool IsQuantitative { get; set; }       
        public bool AutoGenerateSerial { get; set; }
        public long WarehouseId { get; set; }
        public int unitId { get; set; }       
        public SelectList status { get; set; }
        public SelectList Warehouses { get; set; }
        public SelectList Suppliers { get; set; }
        public SelectList CodeType { get; set; }
        //item


        public long InventoryItemId { get; set; }
        [Display(Name = "Supplier")]
        [Required]
        public int VendorId { get; set; } 
        [Display(Name = "Location")]
        public long LocationId { get; set; }  
        [Display(Name = "Status")]
        [Required]
        public int InventoryItemStatusId { get; set; }
        [Display(Name = "Location")]
        public int? InventoryLocationId { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Display(Name = "Unit Cost")]
        [Required]
        public decimal UnitCost { get; set; }
        [Display(Name = "Code Type")]
        public int? CodeTypeId { get; set; }
        public string Code { get; set; }
        [Display(Name = "Issue Number")]
        public string IssueNumber { get; set; }
        public string Notes { get; set; }

        public string PartNumber { get; set; }
        public int? Viscosityid { get; set; }
        public int? StandardTreadDepth { get; set; }        
        public int? TireSize { get; set; }
        public int? TirePattern { get; set; }
        public int?Threshold { get; set; }
        public String SerialNumbers { get; set; }
        public DateTime CreateDT { get; set; }
        //public int? LaneId { get; set; }
        //public int? SubWarehouseId { get; set; }
    }
    
}
