using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Infrastructure.Persistence.Migrations;
using System;
using System.Linq;

namespace PaymentGateway.Infrastructure.Persistence.Extensions
{
    public static class RepositoryExtensions
    {
        public static void SetCamelCase(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                foreach (var p in entity.GetProperties())
                {
                    var columnName = p.GetColumnName(StoreObjectIdentifier.Table(entity.GetTableName(), null));
                    p.SetColumnName(columnName.ToCamelCase());
                }
            }

        }

        public static void ConfigureMigrationSqlLite(this IServiceCollection services, string connectionString)
        {
            services
                // Logging is the replacement for the old IAnnouncer
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Registration of all Fluent Migrator-specific services
                .AddFluentMigratorCore()
                // Configure the runner
                .ConfigureRunner(
                    builder => builder
                        .AddSQLite()
                        .WithGlobalConnectionString(connectionString)
                        // Specify the assembly with the migrations
                        .WithMigrationsIn(typeof(Migration00001).Assembly));
        }


        public static void UseMigrations(this IApplicationBuilder app)
        {
            try
            {
                Console.WriteLine("Migration starter");
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    // Instantiate the runner
                    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                    // Execute the migrations
                    runner.MigrateUp();
                }

                Console.WriteLine("Migration done!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Migration failed", e);
                throw;
            }
        }

        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

    }
}
