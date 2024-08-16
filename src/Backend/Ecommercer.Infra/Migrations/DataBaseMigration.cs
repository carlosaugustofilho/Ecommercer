using Dapper;
using MySqlConnector;

namespace Ecommercer.Domain.Migrations
{
    public class DataBaseMigration
    {
        public static void Migrate(string connectionString, IServiceProvider serviceProvider)
        {
            CriacaoBancoMySql(connectionString);
        }

        private static void CriacaoBancoMySql(string connectionString)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);

            var databaseName = connectionStringBuilder.Database;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();
            parameters.Add("nome", databaseName);

            // Corrigindo o nome da coluna para SCHEMA_NAME
            var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nome", parameters);

            if (!records.Any())
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
        }
    }
}
