using System.Runtime.Serialization;
using DataTransferGraph.Model;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class ParameterModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeModel Type { get; set; }

        public ParameterModel(ParameterDtg parameterModel)
        {
            Name = parameterModel.Name;
            Type = TypeModel.LoadType(parameterModel.Type);
        }
    }
}