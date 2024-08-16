using Ecommercer.Domain.Repositories;
using Ecommercer.Infra.Context;
using Ecommercer.Infra.Datacontext.Repositories;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommercer.Infra
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddContext(services, configuration);
            AddMigrator_MySql(services, configuration);
        }

        private static void AddContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionMySQLServer");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

            services.AddDbContext<DbEcommecer>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

        private static void AddMigrator_MySql(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionMySQLServer");

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddMySql5()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
        }

        public static void ApplyMigrations(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                migrationRunner.ListMigrations();
                migrationRunner.MigrateUp();
            }
        }
    }
}
