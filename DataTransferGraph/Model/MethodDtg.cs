using System.Collections.Generic;
using DataTransferGraph.Enums;

namespace DataTransferGraph.Model
{
    public class MethodDtg
    {
        public string Name { get; set; }

        public List<TypeDtg> GenericArguments { get; set; }

        public AccessLevel Accessibility { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsStatic { get; set; }

        public bool IsVirtual { get; set; }

        public TypeDtg ReturnType { get; set; }

        public bool IsExtensionMethod { get; set; }

        public List<ParameterDtg> Parameters { get; set; }
    }         
}
