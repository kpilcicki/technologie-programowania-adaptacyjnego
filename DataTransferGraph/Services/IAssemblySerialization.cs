using DataTransferGraph.Model;

namespace DataTransferGraph.Services
{
    public interface IAssemblySerialization
    {
        AssemblyDtg Deserialize(string connectionString);
        void Serialize(string connectionString, AssemblyDtg assemblyDtg);
    }
}
