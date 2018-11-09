using System;

namespace Reflection.Exceptions
{
    public class ReflectionException : Exception
    {
        public ReflectionException(string message) : base(message) { }
    }
}