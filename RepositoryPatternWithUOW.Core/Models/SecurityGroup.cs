using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class SecurityGroup
    {
        public SecurityGroup()
        {
            SecurityGroupPermissions = new HashSet<SecurityGroupPermission>();
            SystemUserSecurityGroups = new HashSet<SystemUserSecurityGroup>();
        }

        public int SecurityGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual ICollection<SecurityGroupPermission> SecurityGroupPermissions { get; set; }
        public virtual ICollection<SystemUserSecurityGroup> SystemUserSecurityGroups { get; set; }
    }
}
