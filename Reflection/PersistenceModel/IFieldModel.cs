using System.Runtime.Serialization;

namespace Reflection.PersistenceModel
{
    public interface IFieldModel
    {
       string Name { get; set; }

       ITypeModel Type { get; set; }
    }
}
