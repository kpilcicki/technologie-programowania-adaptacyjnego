using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using DataTransferGraph.Model;
using DataTransferGraph.Services;
using Reflection.Mapper;
using Reflection.Model;

namespace Reflection.Persistence
{
    public class PersistenceManager
    {
        [Import]
        public IAssemblySerialization Serializator = new DatabaseBus.DatabaseConnection();
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

        public static PersistenceManager GetComposedPersistenceManager()
        {
            PersistenceManager pm = new PersistenceManager();

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("../../../plugins"));
            CompositionContainer container = new CompositionContainer(catalog);

            container.ComposeParts(pm);

            return pm;
        }
    }
}
