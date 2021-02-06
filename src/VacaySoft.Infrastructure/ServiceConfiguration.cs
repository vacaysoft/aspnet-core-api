using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacaySoft.Application.Services;
using VacaySoft.Domain.Entities;
using VacaySoft.Infrastructure.Services;

namespace VacaySoft.Infrastructure
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VacaySoftDbContext>(x =>
                x.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddIdentityCore<UserProfile>()
                .AddEntityFrameworkStores<VacaySoftDbContext>();

            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddOptions<IdentityOptions>().Configure(e =>
            {
                e.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 8
                };

                e.User = new UserOptions
                {
                    RequireUniqueEmail = true
                };

                e.ClaimsIdentity = new ClaimsIdentityOptions
                {
                    UserIdClaimType = "Id",
                    UserNameClaimType = "Username"
                };
            });
            return services;
        }
    }
}
