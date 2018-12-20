using System.Reflection;
using DataTransferGraph.Model;

namespace Reflection.Model
{
    public class ParameterModel
    {
        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public ParameterModel(ParameterInfo parameterInfo)
        {
            Name = parameterInfo.Name;
            Type = TypeModel.LoadType(parameterInfo.ParameterType);
        }

        public ParameterModel(ParameterDtg parameterInfo)
        {
            Name = parameterInfo.Name;
            Type = TypeModel.LoadType(parameterInfo.Type);
        }
    }
}