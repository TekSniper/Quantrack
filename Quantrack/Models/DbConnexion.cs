using MySql.Data.MySqlClient;
using Npgsql;

namespace Quantrack.Models
{
    public class DbConnexion
    {
        //private string _connectionString { get; set; }
        private MySqlConnection _connection { get; set; } = new MySqlConnection();
        IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        public MySqlConnection GetConnection()
        {
            _connection.ConnectionString = GetConfiguration().GetSection("ConnectionStrings").GetSection("L€OXPR").Value!;
            return _connection;
        }
    }
}
