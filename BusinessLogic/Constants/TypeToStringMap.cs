using System;
using System.Collections.Generic;
using BusinessLogic.Model;

namespace BusinessLogic.Constants
{
    public static class TypeToStringMap
    {
        public static readonly Dictionary<Type, string> Map = new Dictionary<Type, string>()
        {
            { typeof(TypeTreeItem), "Type" },
            { typeof(PropertyTreeItem), "Property" },
            { typeof(ParameterTreeItem), "Parameter" },
            { typeof(NamespaceTreeItem), "Namespace" },
            { typeof(MethodTreeItem), "Method" },
            { typeof(AssemblyTreeItem), "Type" },
        };
    }
}
