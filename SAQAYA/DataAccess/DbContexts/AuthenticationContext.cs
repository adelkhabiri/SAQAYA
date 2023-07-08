using SAQAYA.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace SAQAYA.DataAccess.DbContexts
{
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext()
        {
        }
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }
        public virtual DbSet<Users> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                IConfiguration config = new ConfigurationBuilder().Add(new Microsoft.Extensions.Configuration.Json.JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
                var test = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(t => t.MarkrtingConsent).HasDefaultValue(0);

            });


            base.OnModelCreating(modelBuilder);
        }

    }
}
