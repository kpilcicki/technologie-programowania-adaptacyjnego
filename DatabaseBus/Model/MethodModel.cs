using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataTransferGraph.Enums;
using DataTransferGraph.Model;

namespace DatabaseBus.Model
{
    public class MethodModel
    {
        public MethodModel()
        {

        }
        public int MethodModelId { get; set; }
        public string Name { get; set; }

        public List<TypeModel> GenericArguments { get; set; }

        public AccessLevel Accessibility { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsStatic { get; set; }

        public bool IsVirtual { get; set; }

        public TypeModel ReturnType { get; set; }

        public bool IsExtensionMethod { get; set; }

        public List<ParameterModel> Parameters { get; set; }

        public MethodModel(MethodDtg methodModel)
        {
            Name = methodModel.Name;
            GenericArguments = methodModel.GenericArguments?.Select(TypeModel.LoadType).ToList();
            Accessibility = methodModel.Accessibility;
            IsAbstract = methodModel.IsAbstract;
            IsStatic = methodModel.IsStatic;
            IsVirtual = methodModel.IsVirtual;
            ReturnType = TypeModel.LoadType(methodModel.ReturnType);
            IsExtensionMethod = methodModel.IsExtensionMethod;
            Parameters = methodModel.Parameters?.Select(p => new ParameterModel(p)).ToList();
        }
    }
}
