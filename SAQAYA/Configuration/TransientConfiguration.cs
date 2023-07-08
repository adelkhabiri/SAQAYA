using SAQAYA.BusinessLogic.IServices;
using SAQAYA.BusinessLogic.Services;
using SAQAYA.DataAccess.DbContexts;
using SAQAYA.EntityFramework.Repository;
using Foundation.AuthApi.Providers;

namespace SAQAYA.Configuration
{
    public static class TransientConfiguration
    {
        public static IServiceCollection AddTransientConfiguration(this IServiceCollection services)
        {
            #region Transient
            // Identity Services
            //services.AddTransient<IUserStore<Users>, CustomUserStore<Users>>();

            //services.AddIdentityCore<AppUsers>()//.AddUserManager<CustomUserManager<Users>>()
            //        //.AddUserStore<CustomUserStore<Users>>() //this one provides data storage for user.
            //    .AddDefaultTokenProviders();

            services.AddTransient<IAccountService, AccountService>();

            services.AddScoped<IUnitOfWork, UnitOfWork<AuthenticationContext>>();

            services.AddTransient<IJwtTokenProvider, JwtTokenProvider>();
            #endregion

            return services;
        }
    }
}
