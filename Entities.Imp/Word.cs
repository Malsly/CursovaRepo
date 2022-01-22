using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Word: IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }

        [ForeignKey("ThemeAndWords")]
        public int? ThemeAndWordsID { get; set; }
        public virtual ThemeAndWords ThemeAndWords { get; set; }
    }
}
