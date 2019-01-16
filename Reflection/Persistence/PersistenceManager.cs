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
        public void Serialize(AssemblyModel assemblyModel)
        {
            AssemblyDtg assemblyDtg = DataTransferMapper.AssemblyDtg(assemblyModel);

            Serializator.Serialize(assemblyDtg);
        }

        public AssemblyModel Deserialize()
        {
            AssemblyDtg assemblyDtg = Serializator.Deserialize();

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
