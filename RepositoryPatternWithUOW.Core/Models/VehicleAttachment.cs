using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class VehicleAttachment
    {
        public long VehicleAttachmentId { get; set; }
        public long VehicleId { get; set; }
        public long? AttachedVehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual Vehicle AttachedVehicle { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
