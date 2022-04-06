using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Brand
    {
        public Brand()
        {
            InventoryItemCategoryBrands = new HashSet<InventoryItemCategoryBrand>();
            InventoryItemTypes = new HashSet<InventoryItemType>();
            Models = new HashSet<Model>();
        }

        public int BrandId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<InventoryItemCategoryBrand> InventoryItemCategoryBrands { get; set; }
        public virtual ICollection<InventoryItemType> InventoryItemTypes { get; set; }
        public virtual ICollection<Model> Models { get; set; }
    }
}
