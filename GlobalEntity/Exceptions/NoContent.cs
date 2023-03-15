using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalEntity.Exceptions
{
    public class NoContent : Exception
    {
        public NoContent(string message) : base(message) { }
    }
}
