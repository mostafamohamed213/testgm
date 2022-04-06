using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryTransaction
    {
        public InventoryTransaction()
        {
            InventoryTransactionDetails = new HashSet<InventoryTransactionDetail>();
        }

        public long InventoryTransactionId { get; set; }
        public long FromLocationId { get; set; }
        public long ToLocationId { get; set; }
        public long SystemUserId { get; set; }
        public long? ObjectId { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual Location FromLocation { get; set; }
        public virtual SystemUser SystemUser { get; set; }
        public virtual Location ToLocation { get; set; }
        public virtual ICollection<InventoryTransactionDetail> InventoryTransactionDetails { get; set; }
    }
}
