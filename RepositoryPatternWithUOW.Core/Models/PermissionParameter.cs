using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class PermissionParameter
    {
        public int PermissionId { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
