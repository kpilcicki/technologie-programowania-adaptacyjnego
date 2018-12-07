using System.Collections.Generic;
using System.Runtime.Serialization;
using Reflection.Enums;

namespace Reflection.PersistenceModel
{
    public interface IMethodModel
    {
        string Name { get; set; }

        List<ITypeModel> GenericArguments { get; set; }

        AccessLevel Accessibility { get; set; }

        bool IsAbstract { get; set; }

        bool IsStatic { get; set; }

        bool IsVirtual { get; set; }

        ITypeModel ReturnType { get; set; }

        bool IsExtensionMethod { get; set; }

        List<IParameterModel> Parameters { get; set; }
    }         
}
