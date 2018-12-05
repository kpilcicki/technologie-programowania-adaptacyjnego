using System.Reflection;

namespace Reflection.Model
{
    public class ParameterModel
    {
        public string Name { get; }

        public TypeModel Type { get; }

        public ParameterModel(ParameterInfo parameterInfo)
        {
            Name = parameterInfo.Name;
            Type = TypeModel.LoadType(parameterInfo.ParameterType);
        }
    }
}