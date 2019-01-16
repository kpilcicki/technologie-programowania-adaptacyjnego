using System.Data.Entity;

namespace DbLogger
{
    public class LoggerContext : DbContext
    {
        public LoggerContext() : base ("name=Logs")
        {
            
        }

        public virtual DbSet<LogItem> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
