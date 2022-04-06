using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class SystemUserPermission
    {
        public long SystemUserId { get; set; }
        public int PermissionId { get; set; }
        public bool IsGranted { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
}
