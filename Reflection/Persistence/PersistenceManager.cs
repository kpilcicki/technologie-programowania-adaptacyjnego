using DataTransferGraph.Model;
using DataTransferGraph.Services;
using FileSerializer;
using Reflection.Mapper;
using Reflection.Model;

namespace Reflection.Persistence
{
    public class PersistenceManager
    {
        public IAssemblySerialization Serializator = new XmlSerializer();
        public void Serialize(AssemblyModel assemblyModel, string connectionString)
        {
            AssemblyDtg assemblyDtg = DataTransferMapper.AssemblyDtg(assemblyModel);

            Serializator.Serialize(connectionString, assemblyDtg);


        }

        public AssemblyModel Deserialize(string connectionString)
        {
            AssemblyDtg assemblyDtg = Serializator.Deserialize(connectionString);

            AssemblyModel assemblyModel = new AssemblyModel(assemblyDtg);

            return assemblyModel;
        }

    }
}
