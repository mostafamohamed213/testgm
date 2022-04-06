using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemCategoryBrand
    {
        public int InventoryItemCategoryId { get; set; }
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual InventoryItemCategory InventoryItemCategory { get; set; }
    }
}
