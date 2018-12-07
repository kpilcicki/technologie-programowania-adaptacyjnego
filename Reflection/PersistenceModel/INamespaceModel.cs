using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Reflection.PersistenceModel
{
    public interface INamespaceModel
    {
        string Name { get; set; }

        List<ITypeModel> Types { get; set; }
    }
}
