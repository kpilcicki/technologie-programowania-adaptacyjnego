using System.Runtime.Serialization;

namespace Reflection.PersistenceModel
{
    public interface IPropertyModel
    {
        string Name { get; set; }

        ITypeModel Type { get; set; }
    }
}