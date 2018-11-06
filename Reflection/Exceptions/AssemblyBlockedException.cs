using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Exceptions
{
    public class AssemblyBlockedException : ReflectionException
    {
        public AssemblyBlockedException(string message) : base(message) {}
    }
}
