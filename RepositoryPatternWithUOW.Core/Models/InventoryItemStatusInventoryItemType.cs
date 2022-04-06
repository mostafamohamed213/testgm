using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemStatusInventoryItemType
    {
        public int InventoryItemStatusId { get; set; }
        public int InventoryItemTypeId { get; set; }

        public virtual InventoryItemStatus InventoryItemStatus { get; set; }
        public virtual InventoryItemType InventoryItemType { get; set; }
    }
}
