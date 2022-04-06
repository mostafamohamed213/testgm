using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class AuthenticationProvider
    {
        public AuthenticationProvider()
        {
            AuthenticationProviderParameters = new HashSet<AuthenticationProviderParameter>();
            SystemUsers = new HashSet<SystemUser>();
        }

        public int AuthenticationProviderId { get; set; }
        public string Name { get; set; }
        public string Library { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual ICollection<AuthenticationProviderParameter> AuthenticationProviderParameters { get; set; }
        public virtual ICollection<SystemUser> SystemUsers { get; set; }
    }
}
