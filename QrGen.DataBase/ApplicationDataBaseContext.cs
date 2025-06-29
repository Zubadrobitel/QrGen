using Microsoft.EntityFrameworkCore;
using QrGen.DataBase.Configurations;
using QrGen.DataBase.Entities;

namespace QrGen.DataBase
{
    public class ApplicationDataBaseContext : DbContext
    {
        public DbSet<QrCodeEntity> QrCodes { get; set; }
        public DbSet<QrInfoEntity> Qrinfos { get; set; }
        public ApplicationDataBaseContext(DbContextOptions<ApplicationDataBaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QrCodeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new QrInfoEntityConfiguration());
        }
    }
}
