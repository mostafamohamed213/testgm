using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class CodeType
    {
        public CodeType()
        {
            InventoryItems = new HashSet<InventoryItem>();
        }

        public int CodeTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
