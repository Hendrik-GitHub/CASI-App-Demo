using Microsoft.EntityFrameworkCore;
using OneStopShop.API.Models.Logging;

namespace OneStopShop.API.Repositries.Logging
{
    public partial class LoggingContext : DbContext
    {
        public virtual DbSet<Log> Log { get; set; }

        public static string ConnectionString { get; set; }

        public static int MessageMaxLength;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.logid).HasColumnName("logid");
                entity.Property(e => e.eventid).HasColumnName("eventid");
                entity.Property(e => e.priority).HasColumnName("priority");
                entity.Property(e => e.severity).HasMaxLength(32);
                entity.Property(e => e.title);
                //entity.Property(e => e.TimeStamp);
                entity.Property(e => e.machinename).HasMaxLength(32);
                entity.Property(e => e.appdomainname).HasMaxLength(512);
                entity.Property(e => e.processid);
                entity.Property(e => e.processname).HasMaxLength(512);
                entity.Property(e => e.threadname).HasMaxLength(512);
                entity.Property(e => e.win32threadid).HasMaxLength(128);
                entity.Property(e => e.message).HasMaxLength(1500);
                entity.Property(e => e.formattedmessage).HasMaxLength(8000);
            });

            MessageMaxLength = 8000;
        }
    }
}
