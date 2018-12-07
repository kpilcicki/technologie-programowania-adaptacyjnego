using System.Runtime.Serialization;

namespace Reflection.PersistenceModel
{
    public interface IParameterModel
    {
        string Name { get; set; }

        ITypeModel Type { get; set; }
    }
}