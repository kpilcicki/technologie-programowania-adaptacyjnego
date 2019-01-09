using DatabaseBus.Mapper;
using DatabaseBus.Model;
using DataTransferGraph.Model;
using DataTransferGraph.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace DatabaseBus
{
    [Export(typeof(IAssemblySerialization))]
    public class DatabaseConnection : IAssemblySerialization
    {
        public AssemblyDtg Deserialize(string connectionString)
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

        public void Serialize(string connectionString, AssemblyDtg assemblyDtg)
        {
            using (AssemblyContext ctx = new AssemblyContext())
            {
                var assemblyModel = new AssemblyModel(assemblyDtg);

                ctx.AssemblyModels.Add(assemblyModel);
                ctx.SaveChanges();
            }
        }
    }
}
