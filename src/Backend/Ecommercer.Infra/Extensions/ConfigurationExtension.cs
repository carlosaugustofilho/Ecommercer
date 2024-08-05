using Microsoft.Azure.Management.AppService.Fluent.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommercer.Infra.Extensions
{
    public static class ConfigurationExtension
    {
        public static bool IsUnitTestEnviroment(this IConfiguration configurarion) => configurarion.GetValue<bool>("InMemoryTest");

        public static DatabaseType DatabaseType(this IConfiguration configurarion)
        {
            var databaseType = configurarion.GetConnectionString("DatabaseType");

            return (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseType!);
        }

        public static string ConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("ConnectionMySQLServer")!;
        }
    }
}
