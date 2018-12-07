using Reflection.PersistenceModel;
using System.Reflection;

namespace Reflection.Model
{
    public class ParameterModel : IParameterModel
    {
        public string Name { get; set; }

        public ITypeModel Type { get; set; }

        public ParameterModel(ParameterInfo parameterInfo)
        {
            Name = parameterInfo.Name;
            Type = TypeModel.LoadType(parameterInfo.ParameterType);
        }

        public ParameterModel(IParameterModel parameterInfo)
        {
            Name = parameterInfo.Name;
            Type = TypeModel.LoadType(parameterInfo.Type);
        }
    }
}