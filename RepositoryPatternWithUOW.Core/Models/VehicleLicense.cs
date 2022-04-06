using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class VehicleLicense
    {
        public long VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
