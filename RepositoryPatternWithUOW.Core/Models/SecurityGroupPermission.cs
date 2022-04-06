using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class SecurityGroupPermission
    {
        public int SecurityGroupId { get; set; }
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual SecurityGroup SecurityGroup { get; set; }
    }
}
