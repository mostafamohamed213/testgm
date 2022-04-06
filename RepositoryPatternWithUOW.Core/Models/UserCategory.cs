using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class UserCategory
    {
        public UserCategory()
        {
            InverseParentUserCategory = new HashSet<UserCategory>();
            SystemUsers = new HashSet<SystemUser>();
        }

        public int UserCategoryId { get; set; }
        public string Name { get; set; }
        public int? ParentUserCategoryId { get; set; }

        public virtual UserCategory ParentUserCategory { get; set; }
        public virtual ICollection<UserCategory> InverseParentUserCategory { get; set; }
        public virtual ICollection<SystemUser> SystemUsers { get; set; }
    }
}
