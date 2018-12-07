using Reflection.PersistenceModel;
using System.Runtime.Serialization;

namespace FileSerializer.Model
{
    [DataContract(IsReference = true)]
    public class ParameterModel : IParameterModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ITypeModel Type { get; set; }

        public ParameterModel(IParameterModel parameterModel)
        {
            Name = parameterModel.Name;
            Type = TypeModel.LoadType(parameterModel.Type) as ITypeModel;
        }
    }
}