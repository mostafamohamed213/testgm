using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class SystemUserLanguage
    {
        public long SystemUserId { get; set; }
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
    }
}
