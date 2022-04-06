using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class AuthenticationProviderParameter
    {
        public int AuthenticationProviderId { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }

        public virtual AuthenticationProvider AuthenticationProvider { get; set; }
    }
}
