using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class SystemUserTerminal
    {
        public long SystemUserId { get; set; }
        public int TerminalId { get; set; }

        public virtual SystemUser SystemUser { get; set; }
        public virtual Terminal Terminal { get; set; }
    }
}
