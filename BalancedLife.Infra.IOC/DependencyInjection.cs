using BalancedLife.Application.interfaces;
using BalancedLife.Application.Mappings;
using BalancedLife.Application.Services;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using BalancedLife.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BalancedLife.Infra.IOC {
    public static class DependencyInjection {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            services.AddScoped<IUserInfoRepository, UserInfoRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(typeof(UserMapper));

            return services;
        }
    }
}
