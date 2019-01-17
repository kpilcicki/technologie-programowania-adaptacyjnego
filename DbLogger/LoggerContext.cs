using System.Data.Entity;

namespace DbLogger
{
    public class LoggerContext : DbContext
    {
        public LoggerContext() : base ("name=Logs")
        {
            Database.CommandTimeout = 900;
        }

        public virtual DbSet<LogItem> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
