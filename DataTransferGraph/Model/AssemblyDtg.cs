using System.Collections.Generic;

namespace DataTransferGraph.Model
{
    public class AssemblyDtg
    {
       public string Name { get; set; }

       public List<NamespaceDtg> NamespaceModels { get; set; }
    }
}
