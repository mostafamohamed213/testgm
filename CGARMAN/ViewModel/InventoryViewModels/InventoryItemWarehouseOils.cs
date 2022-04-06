﻿using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.InventoryViewModels
{
    public class InventoryItemWarehouseOils
    {
        public long? InventoryItemId { get; set; }
        public int InventoryItemTypeId { get; set; }
        public long WarehouseId { get; set; }

        [Display(Name = "Supplier")]
        [Required]
        public int VendorId { get; set; }
        //[Display(Name = "Location")]
        //public long LocationId { get; set; }
        [Display(Name = "Status")]
        [Required]
        public int InventoryItemStatusId { get; set; }

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
        public string SerialNumbers { get; set; }
        public int Viscosity { get; set; }
       
        public InventoryItem display { get; set; }
        public string ViscosityStr { get; internal set; }
    }
}
