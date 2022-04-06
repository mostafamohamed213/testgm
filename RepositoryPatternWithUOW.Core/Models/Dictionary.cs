using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryPatternWithUOW.Core.Models
{
    public partial class Dictionary
    {
        public int LanguageId { get; set; }
        public long PageId { get; set; }
        public string String { get; set; }
        public string Value { get; set; }

        public virtual Language Language { get; set; }
        public virtual Page Page { get; set; }
    }
}
