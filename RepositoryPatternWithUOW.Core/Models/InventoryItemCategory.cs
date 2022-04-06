using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemCategory
    {
        public InventoryItemCategory()
        {
            InventoryItemCategoryBrands = new HashSet<InventoryItemCategoryBrand>();
            InventoryItemTypes = new HashSet<InventoryItemType>();
            InverseParentInventoryItemCategory = new HashSet<InventoryItemCategory>();
        }

        public int InventoryItemCategoryId { get; set; }
        public string Name { get; set; }
        public int? ParentInventoryItemCategoryId { get; set; }

        public virtual InventoryItemCategory ParentInventoryItemCategory { get; set; }
        public virtual ICollection<InventoryItemCategoryBrand> InventoryItemCategoryBrands { get; set; }
        public virtual ICollection<InventoryItemType> InventoryItemTypes { get; set; }
        public virtual ICollection<InventoryItemCategory> InverseParentInventoryItemCategory { get; set; }
    }
}
