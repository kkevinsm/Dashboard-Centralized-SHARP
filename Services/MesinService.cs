using MySql.Data.MySqlClient;
using Dapper;
using CentralizedDashboard.Models; // Pastikan ini sesuai dengan namespace Model di atas

namespace CentralizedDashboard.Services
{
    public class MesinService
    {

        private string connectionString = "Server=localhost;Port=3306;Database=monitoring_centralized;User=root;Password=;";

        public List<ErrorLog> GetLatestLogs()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM error_logs ORDER BY waktu DESC LIMIT 20";
                
                var hasil = connection.Query<ErrorLog>(sql).ToList();
                return hasil;
            }
        }
    }
}