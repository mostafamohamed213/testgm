using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryLog
    {
        public long InventoryLogID { get; set; }
        public string SystemUserID { get; set; }
        public DateTime CreateDT { get; set; }
        public int InventoryLogTableID { get; set; }
        public int InventoryLogOperationID { get; set; }
        public string Object1 { get; set; }
        public string Object2 { get; set; }
        public string Object3 { get; set; }
        public string Description { get; set; }
        public long ObjectID { get; set; }

        public virtual InventoryLogOperation InventoryLogOperation { get; set; }
        public virtual InventoryLogTable InventoryLogTable { get; set; }
    }
}
