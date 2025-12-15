using MySql.Data.MySqlClient;
using Dapper;
using CentralizedDashboard.Models; 

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

                // Ambil data terbaru untuk setiap mesin (GROUP BY nama_mesin_proses)
                string sql = @"
                    SELECT t1.* 
                    FROM centralized_logs t1
                    INNER JOIN (
                        SELECT nama_mesin_proses, MAX(waktu) as latest_time
                        FROM centralized_logs
                        GROUP BY nama_mesin_proses
                    ) t2 
                    ON t1.nama_mesin_proses = t2.nama_mesin_proses 
                    AND t1.waktu = t2.latest_time
                    ORDER BY t1.waktu DESC";
                
                var hasil = connection.Query<ErrorLog>(sql).ToList();
                return hasil;
            }
        }
    }
}