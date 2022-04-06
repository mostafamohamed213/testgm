using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.InventoryViewModels
{
    public class CreateInventoryItemTypeViewModel
    {
        public int InventoryItemTypeId { get; set; }
        [Required]
        public long WarehouseId { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }
        [Display(Name = "Classification")]
        public int InventoryItemTypeClassificationId { get; set; }
        [Display(Name = "Unit")]
        public int InventoryItemTypeUnitId { get; set; }
        [Display(Name = "Cost Center")]
        public int CostCenterId { get; set; }
        public bool IsQuantitative { get; set; }
        [Display(Name = "Auto Generate Serial")]
        public bool AutoGenerateSerial { get; set; }
        [Display(Name = "Brand")]      
        public int BrandId { get; set; }
        public int? InventoryitemcategoryId { get; set; }
        public int? ModelId { get; set; }
        public string UrlImage { get; set; }
        public IFormFile ItemImage { get; set; }
    }
}
