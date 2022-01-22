using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abs
{
    public interface IPerson: IEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}
