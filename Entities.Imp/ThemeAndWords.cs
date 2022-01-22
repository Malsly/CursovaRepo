using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ThemeAndWords: IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Theme { get; set; }
        public virtual ICollection<Word> Words { get; set; }
    }
}
