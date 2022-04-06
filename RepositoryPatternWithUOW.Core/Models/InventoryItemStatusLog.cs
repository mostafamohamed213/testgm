using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemStatusLog
    {
        public long InventoryItemId { get; set; }
        public DateTime StatusDts { get; set; }
        public int InventoryItemStatusId { get; set; }

        public virtual InventoryItem InventoryItem { get; set; }
        public virtual InventoryItemStatus InventoryItemStatus { get; set; }
    }
}
