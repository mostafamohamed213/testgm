using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class InventoryItem
    {
        public InventoryItem()
        {
            InventoryItemAssignments = new HashSet<InventoryItemAssignment>();
            InventoryItemReservations = new HashSet<InventoryItemReservation>();
            InventoryItemStatusLogs = new HashSet<InventoryItemStatusLog>();
        }

        public long InventoryItemId { get; set; }
        public string SerialNumber { get; set; }
        public int VendorId { get; set; }
        public long LocationId { get; set; }
        public long SystemUserId { get; set; }
        public DateTime CreateDts { get; set; }
        public int InventoryItemTypeId { get; set; }
        public int InventoryItemStatusId { get; set; }
        public int? InventoryLocationId { get; set; }
        public int? BrandId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public int? CodeTypeId { get; set; }
        public string Code { get; set; }
        public string IssueNumber { get; set; }
        public string Notes { get; set; }
        public decimal CurrentQuantity { get; set; }

        public virtual CodeType CodeType { get; set; }
        public virtual InventoryItemStatus InventoryItemStatus { get; set; }
        public virtual InventoryItemType InventoryItemType { get; set; }
        public virtual InventoryLocation InventoryLocation { get; set; }
        public virtual Location Location { get; set; }
        public virtual SystemUser SystemUser { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<InventoryItemAssignment> InventoryItemAssignments { get; set; }
        public virtual ICollection<InventoryItemReservation> InventoryItemReservations { get; set; }
        public virtual ICollection<InventoryItemStatusLog> InventoryItemStatusLogs { get; set; }
    }
}
