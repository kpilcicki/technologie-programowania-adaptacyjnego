using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using DataTransferGraph.Exception;
using DataTransferGraph.Model;
using DataTransferGraph.Services;
using Reflection.Exceptions;
using Reflection.Mapper;
using Reflection.Model;

namespace Reflection.Persistence
{
    public class PersistenceManager
    {
        [Import]
        public IAssemblyPersist Serializator { get; set; }
        public void Serialize(AssemblyModel assemblyModel)
        {
            try
            {
                AssemblyDtg assemblyDtg = DataTransferMapper.AssemblyDtg(assemblyModel);

                Serializator.Persist(assemblyDtg);
            }
            catch (SavingMetadataException e)
            {
                throw new PersistenceException("Saving metadata to data source failed.", e);
            }
            
        }

        public AssemblyModel Deserialize()
        {
            try
            {
                AssemblyDtg assemblyDtg = Serializator.Read();

                AssemblyModel assemblyModel = new AssemblyModel(assemblyDtg);

                return assemblyModel;
            }
            catch (ReadingMetadataException e)
            {
                throw new PersistenceException("Reading metadata from data source failed.", e);
            }
            
        }
    }
}
