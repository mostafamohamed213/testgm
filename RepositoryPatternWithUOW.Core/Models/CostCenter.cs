using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class CostCenter
    {
        public CostCenter()
        {
            Technicians = new HashSet<Technician>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int CostCenterId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime CreateDts { get; set; }
        public bool Enable { get; set; }
        [Required]
        public string Systemusercrate { get; set; }

        public virtual ICollection<Technician> Technicians { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
