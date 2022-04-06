using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class TechnicianCompany
    {
        public TechnicianCompany()
        {
            Technicians = new HashSet<Technician>();
        }

        public int TechnicianCompanyId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDts { get; set; }
        public bool Enable { get; set; }
        public virtual ICollection<Technician> Technicians { get; set; }
    }
}
