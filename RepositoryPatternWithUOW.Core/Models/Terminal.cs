using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Terminal
    {
        public Terminal()
        {
            SystemUserTerminals = new HashSet<SystemUserTerminal>();
        }

        public int TerminalId { get; set; }
        public string Hostname { get; set; }
        public string Ip { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual ICollection<SystemUserTerminal> SystemUserTerminals { get; set; }
    }
}
