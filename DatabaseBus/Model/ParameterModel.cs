using System.Runtime.Serialization;
using DataTransferGraph.Model;

namespace DatabaseBus.Model
{
    public class ParameterModel
    {
        public ParameterModel()
        {
                
        }
        public int ParameterModelId { get; set; }
        public string Name { get; set; }

        public TypeModel Type { get; set; }

        public ParameterModel(ParameterDtg parameterModel)
        {
            Name = parameterModel.Name;
            Type = TypeModel.LoadType(parameterModel.Type);
        }
    }
}