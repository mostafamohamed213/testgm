using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryTransactionDetail
    {
        public long InventoryTransactionId { get; set; }
        public long InventoryItemId { get; set; }
        public int InventoryItemStatusId { get; set; }
        public decimal? Quantity { get; set; }

        public virtual InventoryItemStatus InventoryItemStatus { get; set; }
        public virtual InventoryTransaction InventoryTransaction { get; set; }
    }
}
