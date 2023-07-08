using Microsoft.EntityFrameworkCore;
using SAQAYA.BusinessLogic.Models.Settings;
using SAQAYA.DataAccess.DbContexts;

namespace SAQAYA.Configuration
{
    public static class SettingsConfiguration
    {
        public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(connectionString));

            services.Configure<AuthenticationConfiguration>(configuration.GetSection(nameof(AuthenticationConfiguration)));
            return services;
        }
    }
}
