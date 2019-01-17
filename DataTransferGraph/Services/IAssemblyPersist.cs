using DataTransferGraph.Model;

namespace DataTransferGraph.Services
{
    public interface IAssemblyPersist
    {
        AssemblyDtg Read();
        void Persist(AssemblyDtg assemblyDtg);
    }
}
