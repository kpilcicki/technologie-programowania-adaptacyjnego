using DatabaseBus.Mapper;
using DatabaseBus.Model;
using DataTransferGraph.Model;
using DataTransferGraph.Services;
using System.Linq;
using System.ComponentModel.Composition;

namespace DatabaseBus
{
    [Export(typeof(IAssemblySerialization))]
    public class DatabaseConnection : IAssemblySerialization
    {
        public AssemblyDtg Deserialize()
        {
            using (AssemblyContext ctx = new AssemblyContext())
            {
                var assemblyFromDb = ctx.AssemblyModels.First();
                if (assemblyFromDb == null)
                {
                    return null;
                }
                return DataTransferMapper.AssemblyDtg(assemblyFromDb);
            }
        }

        public void Serialize(AssemblyDtg assemblyDtg)
        {
            using (AssemblyContext ctx = new AssemblyContext())
            {
                var assemblyModel = new AssemblyModel(assemblyDtg);

                ctx.AssemblyModels.Add(assemblyModel);
                ctx.SaveChanges();
            }
        }

//        private string GetValidConnectionString()
//        {
//            string connectionString = ConfigurationManager.AppSettings["connectionString"];
//
//            if (string.IsNullOrEmpty(connectionString))
//            {
//                throw new SavingMetadataException("Provided connection string to database is null or empty");
//            }
//
//            return connectionString;
//        }
    }
}
