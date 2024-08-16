using Microsoft.Extensions.Configuration;

public static class ConfigurationExtension
{
    public static bool IsUnitTestEnviroment(this IConfiguration configurarion)
        => configurarion.GetValue<bool>("InMemoryTest");

    public static string ConnectionString(this IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionMySQLServer");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A string de conexão para 'ConnectionMySQLServer' está ausente ou é nula." +
                                                 " Por favor, certifique-se de que ela está definida no arquivo de configuração.");
        }
        return connectionString;
    }

}
