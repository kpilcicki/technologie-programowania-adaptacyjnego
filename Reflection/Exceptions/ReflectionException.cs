using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Exceptions
{
    public class ReflectionException : Exception
    {
        public ReflectionException(string message) : base(message) {}
    }
}
