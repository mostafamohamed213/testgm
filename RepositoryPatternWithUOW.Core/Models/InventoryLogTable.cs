using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryLogTable
    {
        public InventoryLogTable()
        {
            InventoryLogs = new HashSet<InventoryLog>();
        }

        public int InventoryLogTableId { get; set; }
        public string TableName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<InventoryLog> InventoryLogs { get; set; }
    }
}
