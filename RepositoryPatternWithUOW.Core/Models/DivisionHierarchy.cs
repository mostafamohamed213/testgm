using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class DivisionHierarchy
    {
        public int? DivisionLevelId { get; set; }
        public int? CountryId { get; set; }
        public long? DivisionId { get; set; }
        public long? ParentDivisionId { get; set; }
    }
}
