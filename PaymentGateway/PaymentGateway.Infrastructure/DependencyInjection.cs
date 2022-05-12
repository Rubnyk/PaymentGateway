using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Infrastructure.Persistence;
using PaymentGateway.Infrastructure.Persistence.Extensions;
using PaymentGateway.Infrastructure.Services;
using System;

namespace PaymentGateway.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Sqllite");
            services.AddDbContext<RepositoryContext>(o => o
                .UseSqlite(connection)
                .EnableSensitiveDataLogging()
            );
            services.ConfigureMigrationSqlLite(connection);
            services.AddScoped((Func<IServiceProvider, IRepositoryContext>)(provider => provider.GetService<RepositoryContext>()));
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
