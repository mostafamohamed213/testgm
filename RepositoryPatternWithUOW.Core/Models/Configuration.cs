using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Configuration
    {
        public string ConfigurationCode { get; set; }
        public int ConfigurationCategoryId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string SecurityHash { get; set; }

        public virtual ConfigurationCategory ConfigurationCategory { get; set; }
    }
}
