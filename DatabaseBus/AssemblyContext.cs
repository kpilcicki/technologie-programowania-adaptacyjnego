using DatabaseBus.Model;
using System.Data.Entity;

namespace DatabaseBus
{
    class AssemblyContext : DbContext
    {
        public AssemblyContext(): base("name=AssemblyDatabase")
        {
            Database.CommandTimeout = 900;
            Database.SetInitializer<AssemblyContext>(new DropCreateDatabaseAlways<AssemblyContext>());
        }

        public virtual DbSet<AssemblyModel> AssemblyModels { get; set; }
        public virtual DbSet<FieldModel> FieldModels { get; set; }
        public virtual DbSet<MethodModel> MethodModels { get; set; }
        public virtual DbSet<NamespaceModel> NamespaceModels { get; set; }
        public virtual DbSet<ParameterModel> ParameterModels { get; set; }
        public virtual DbSet<PropertyModel> PropertyModels { get; set; }
        public virtual DbSet<TypeModel> TypeModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

    }
}
