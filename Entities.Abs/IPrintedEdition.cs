using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abs
{
    public interface IPrintedEdition
    {
        public string Name { get; set; }
        public float Rate { get; set; }
    }
}
