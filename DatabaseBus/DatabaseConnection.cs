using DatabaseBus.Mapper;
using DatabaseBus.Model;
using DataTransferGraph.Model;
using DataTransferGraph.Services;
using System.Linq;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Media;

namespace DatabaseBus
{
    [Export(typeof(IAssemblyPersist))]
    public class DatabaseConnection : IAssemblyPersist
    {
        public AssemblyDtg Read()
        {
            using (AssemblyContext ctx = new AssemblyContext())
            {
                ctx.FieldModels.Load();
                ctx.MethodModels.Load();
                ctx.NamespaceModels.Load();
                ctx.ParameterModels.Load();
                ctx.PropertyModels.Load();
                ctx.TypeModels.Load();
                var assemblyFromDb = ctx.AssemblyModels.First();
                if (assemblyFromDb == null)
                {
                    return null;
                }
                return DataTransferMapper.AssemblyDtg(assemblyFromDb);
            }
        }

        public void Persist(AssemblyDtg assemblyDtg)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<AssemblyContext>());
            using (AssemblyContext ctx = new AssemblyContext())
            {
                var assemblyModel = new AssemblyModel(assemblyDtg);

                ctx.AssemblyModels.Add(assemblyModel);
                ctx.SaveChanges();
                SystemSounds.Question.Play();
            }
        }
    }
}
