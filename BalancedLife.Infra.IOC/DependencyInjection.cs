using BalancedLife.Application.interfaces;
using BalancedLife.Application.Interfaces;
using BalancedLife.Application.Mappings;
using BalancedLife.Application.Services;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using BalancedLife.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BalancedLife.Infra.IOC {
    public static class DependencyInjection {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Registro de repositórios
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<ISnackRepository, SnackRepository>();
            services.AddScoped<IBodyRepository, BodyRepository>();
            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IPasswordResetCodeRepository, PasswordResetCodeRepository>();
            services.AddScoped<IPatientRepository, PatientLinkRepository>();
            services.AddScoped<IDietRepository, DietRepository>();

            // Registro de serviços de aplicação
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<ISnackService, SnackService>();
            services.AddScoped<IBodyService, BodyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDietService, DietService>();

            // Registro do AutoMapper
            services.AddAutoMapper(typeof(UserMapper));

            // Registro do serviço de e-mail
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            services.AddTransient<IEmailService, EmailService>(sp => {
                var smtpSettings = sp.GetRequiredService<IOptions<SmtpSettings>>().Value;
                return new EmailService(smtpSettings);
            });

            return services;
        }
    }
}
