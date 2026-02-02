using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SLACalc.Application.Common.Interfaces;
using SLACalc.Domain.Interfaces;
using SLACalc.Infrastructure.Data;
using SLACalc.Infrastructure.Repositories;
using SLACalc.Infrastructure.Services;


namespace SlaCalculation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Repositories
            services.AddScoped<IBusinessHourRepository, BusinessHourRepository>();
            services.AddScoped<IBusinessClosureRepository, BusinessClosureRepository>();
            services.AddScoped<ISlaConfigurationRepository, SlaConfigurationRepository>();

            // Services
            services.AddScoped<IBusinessHourCalculator, BusinessHourCalculator>();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();

            return services;
        }
    }
}