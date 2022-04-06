using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class SystemUser
    {
        public SystemUser()
        {
            InventoryItems = new HashSet<InventoryItem>();
            InventoryTransactions = new HashSet<InventoryTransaction>();
            Logs = new HashSet<Log>();
            SystemUserPermissions = new HashSet<SystemUserPermission>();
            SystemUserSecurityGroups = new HashSet<SystemUserSecurityGroup>();
            SystemUserTerminals = new HashSet<SystemUserTerminal>();
        }

        public long SystemUserId { get; set; }
        public string Username { get; set; }
        public int AuthenticationProviderId { get; set; }
        public string Password { get; set; }
        public int UserCategoryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool RestrictTerminals { get; set; }
        public int FailedAttemptsCount { get; set; }
        public DateTime? LockDts { get; set; }
        public bool PasswordChanged { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreateDts { get; set; }

        public virtual AuthenticationProvider AuthenticationProvider { get; set; }
        public virtual UserCategory UserCategory { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<SystemUserPermission> SystemUserPermissions { get; set; }
        public virtual ICollection<SystemUserSecurityGroup> SystemUserSecurityGroups { get; set; }
        public virtual ICollection<SystemUserTerminal> SystemUserTerminals { get; set; }
    }
}
