using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class SystemUserSecurityGroup
    {
        public long SystemUserId { get; set; }
        public int SecurityGroupId { get; set; }

        public virtual SecurityGroup SecurityGroup { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
}
