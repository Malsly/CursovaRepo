using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Result<T> where T : class
    {
        public string Message { get; set; }
        public Status Status { get; set; }
        public T Value { get; set; }
    }
}
