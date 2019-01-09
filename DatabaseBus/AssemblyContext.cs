using DatabaseBus.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace DatabaseBus
{
    class AssemblyContext : DbContext
    {
        public AssemblyContext(): base("name=AssemblyDatabase")
        {
            Database.SetInitializer<AssemblyContext>(new DropCreateDatabaseIfModelChanges<AssemblyContext>());
        }

        public DbSet<AssemblyModel> AssemblyModels { get; set; }
        public DbSet<FieldModel> FieldModels { get; set; }
        public DbSet<MethodModel> MethodModels { get; set; }
        public DbSet<NamespaceModel> NamespaceModels { get; set; }
        public DbSet<ParameterModel> ParameterModels { get; set; }
        public DbSet<PropertyModel> PropertyModels { get; set; }
        public DbSet<TypeModel> TypeModels { get; set; }

    }
}
