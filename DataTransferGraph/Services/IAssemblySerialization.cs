using DataTransferGraph.Model;

namespace DataTransferGraph.Services
{
    public interface IAssemblySerialization
    {
        AssemblyDtg Deserialize();
        void Serialize(AssemblyDtg assemblyDtg);
    }
}
