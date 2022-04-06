using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Permission
    {
        public Permission()
        {
            PermissionParameters = new HashSet<PermissionParameter>();
            SecurityGroupPermissions = new HashSet<SecurityGroupPermission>();
            SystemUserPermissions = new HashSet<SystemUserPermission>();
        }

        public int PermissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual ICollection<PermissionParameter> PermissionParameters { get; set; }
        public virtual ICollection<SecurityGroupPermission> SecurityGroupPermissions { get; set; }
        public virtual ICollection<SystemUserPermission> SystemUserPermissions { get; set; }
    }
}
