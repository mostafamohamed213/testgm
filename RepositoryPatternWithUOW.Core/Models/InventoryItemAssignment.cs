using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemAssignment
    {
        public long InventoryItemId { get; set; }
        public long ObjectId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime AssignmentDts { get; set; }

        public virtual InventoryItem InventoryItem { get; set; }
    }
}
