using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class DivisionLevel
    {
        public int DivisionLevelId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}
