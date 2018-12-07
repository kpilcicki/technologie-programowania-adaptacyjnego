using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Reflection.PersistenceModel
{
    public interface IAssemblyModel
    {
       string Name { get; set; }

       List<INamespaceModel> NamespaceModels { get; set; }
    }
}
