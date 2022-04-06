using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryLogOperation
    {
        public InventoryLogOperation()
        {
            InventoryLogs = new HashSet<InventoryLog>();
        }

        public int InventoryLogOperationId { get; set; }
        public string OperationName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<InventoryLog> InventoryLogs { get; set; }
    }
}
