using MySql.Data.MySqlClient;
using Npgsql;

namespace Quantrack.Models
{
    public class DbConnexion
    {
        //private string _connectionString { get; set; }
        private NpgsqlConnection _connection { get; set; } = new NpgsqlConnection();
        IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        public NpgsqlConnection GetConnection()
        {
            _connection.ConnectionString = GetConfiguration().GetSection("ConnectionStrings").GetSection("L€OPGS").Value!;
            return _connection;
        }
    }
}
