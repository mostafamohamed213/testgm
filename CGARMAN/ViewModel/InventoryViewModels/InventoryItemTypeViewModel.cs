using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.InventoryViewModels
{
    public class InventoryItemTypeViewModel
    {
        public int InventoryItemTypeId { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }
        [Display(Name = "Classification")]
        public int InventoryItemTypeClassificationId { get; set; }
        [Display(Name = "Unit")]
        public int? InventoryItemTypeUnitId { get; set; }
        [Display(Name = "Cost Center")]
        public int? CostCenterId { get; set; }
        public bool IsQuantitative { get; set; }
        [Display(Name = "Auto Generate Serial")]
        public bool AutoGenerateSerial { get; set; }
        [Display(Name = "Product Picture")]
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Inventoryitemcategory { get; set; }
        public string Warehouse { get; set; }
        public bool IsComeFromPopup{ get; set; }
        public IFormFile ItemImage { get; set; }
        public SelectList CostCenters { get; set; }
        public SelectList Units { get; set; }
        public SelectList Classifications { get; set; }
    }
}
