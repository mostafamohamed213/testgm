using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemStatus
    {
        public InventoryItemStatus()
        {
            InventoryItemStatusInventoryItemTypes = new HashSet<InventoryItemStatusInventoryItemType>();
            InventoryItemStatusLogs = new HashSet<InventoryItemStatusLog>();
            InventoryItems = new HashSet<InventoryItem>();
            InventoryTransactionDetails = new HashSet<InventoryTransactionDetail>();
        }

        public int InventoryItemStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<InventoryItemStatusInventoryItemType> InventoryItemStatusInventoryItemTypes { get; set; }
        public virtual ICollection<InventoryItemStatusLog> InventoryItemStatusLogs { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
        public virtual ICollection<InventoryTransactionDetail> InventoryTransactionDetails { get; set; }
    }
}
