using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Language
    {
        public Language()
        {
            Dictionaries = new HashSet<Dictionary>();
            SystemUserLanguages = new HashSet<SystemUserLanguage>();
        }

        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public bool Rtl { get; set; }

        public virtual ICollection<Dictionary> Dictionaries { get; set; }
        public virtual ICollection<SystemUserLanguage> SystemUserLanguages { get; set; }
    }
}
