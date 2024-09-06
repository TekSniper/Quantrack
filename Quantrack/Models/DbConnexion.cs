using MySql.Data.MySqlClient;
using Npgsql;

namespace Quantrack.Models
{
    public class DbConnexion
    {
        //private string _connectionString { get; set; }
        private MySqlConnection _connection { get; set; } = new MySqlConnection();
        private NpgsqlConnection _connection_1 { get; set; } = new NpgsqlConnection();
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        public MySqlConnection GetConnection()
        {
            _connection.ConnectionString = GetConfiguration().GetSection("ConnectionStrings").GetSection("L€OXPR").Value!;
            return _connection;
        }
        public NpgsqlConnection GetConnection_1()
        {
            _connection_1.ConnectionString = GetConfiguration().GetSection("ConnectionStrings").GetSection("L€OPGS").Value!;
            return _connection_1;
        }
    }
}
