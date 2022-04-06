using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Model
    {
        public Model()
        {
            InventoryItemTypes = new HashSet<InventoryItemType>();
        }

        public int ModelId { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<InventoryItemType> InventoryItemTypes { get; set; }
    }
}
