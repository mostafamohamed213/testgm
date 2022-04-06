using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Page
    {
        public Page()
        {
            Dictionaries = new HashSet<Dictionary>();
        }

        public long PageId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Dictionary> Dictionaries { get; set; }
    }
}
