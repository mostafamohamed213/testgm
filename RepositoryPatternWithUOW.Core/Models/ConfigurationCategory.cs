using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class ConfigurationCategory
    {
        public ConfigurationCategory()
        {
            Configurations = new HashSet<Configuration>();
            InverseParentConfigurationCategory = new HashSet<ConfigurationCategory>();
        }

        public int ConfigurationCategoryId { get; set; }
        public string Name { get; set; }
        public int? ParentConfigurationCategoryId { get; set; }

        public virtual ConfigurationCategory ParentConfigurationCategory { get; set; }
        public virtual ICollection<Configuration> Configurations { get; set; }
        public virtual ICollection<ConfigurationCategory> InverseParentConfigurationCategory { get; set; }
    }
}
