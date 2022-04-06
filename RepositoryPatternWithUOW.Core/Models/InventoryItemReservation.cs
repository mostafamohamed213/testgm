using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItemReservation
    {
        public long InventoryItemId { get; set; }
        public long ReservingObjectId { get; set; }
        public decimal ReservedQuantity { get; set; }
        public DateTime ReservingDts { get; set; }

        public virtual InventoryItem InventoryItem { get; set; }
    }
}
