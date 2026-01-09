using MySql.Data.MySqlClient;
using Dapper;
using CentralizedDashboard.Models;

namespace CentralizedDashboard.Services
{
    public class ShiftAccumulationService
    {
        private readonly IConfiguration _configuration;

        public ShiftAccumulationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        // Buat tabel jika belum ada
        public void InitializeTable()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS `shift_accumulation_A` (
                            `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                            `date` DATE NOT NULL,
                            `shift_number` INT NOT NULL,
                            `total_seconds` INT NOT NULL DEFAULT 0,
                            `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                            UNIQUE KEY `unique_date_shift` (`date`, `shift_number`)
                        );
                    ";
                    
                    using (MySqlCommand cmd = new MySqlCommand(createTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing table: {ex.Message}");
            }
        }

        // GET akumulasi untuk tanggal & shift tertentu
        public ShiftAccumulation? GetAccumulation(DateTime date, int shiftNumber)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT id, date, shift_number, total_seconds, updated_at
                        FROM shift_accumulation_a
                        WHERE date = @date AND shift_number = @shiftNumber
                    ";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@shiftNumber", shiftNumber);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ShiftAccumulation
                                {
                                    Id = (int)reader["id"],
                                    Date = ((DateTime)reader["date"]).Date,
                                    ShiftNumber = (int)reader["shift_number"],
                                    TotalSeconds = (int)reader["total_seconds"],
                                    UpdatedAt = (DateTime)reader["updated_at"]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting accumulation: {ex.Message}");
            }

            return null;
        }

        // SAVE atau UPDATE akumulasi
        public void SaveAccumulation(DateTime date, int shiftNumber, int totalSeconds)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    // Cek apakah sudah ada record
                    var existing = GetAccumulation(date, shiftNumber);

                    if (existing == null)
                    {
                        // INSERT baru
                        string insertQuery = @"
                            INSERT INTO shift_accumulation_a (date, shift_number, total_seconds)
                            VALUES (@date, @shiftNumber, @totalSeconds)
                        ";

                        using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@shiftNumber", shiftNumber);
                            cmd.Parameters.AddWithValue("@totalSeconds", totalSeconds);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // UPDATE existing - TAMBAH nilai baru ke total
                        string updateQuery = @"
                            UPDATE shift_accumulation_a
                            SET total_seconds = total_seconds + @addSeconds,
                                updated_at = NOW()
                            WHERE date = @date AND shift_number = @shiftNumber
                        ";

                        using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@addSeconds", totalSeconds);
                            cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@shiftNumber", shiftNumber);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving accumulation: {ex.Message}");
            }
        }

        
        public List<ShiftAccumulation> GetLast7DaysAccumulation()
        {
            var result = new List<ShiftAccumulation>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT id, date, shift_number, total_seconds, updated_at
                        FROM shift_accumulation_a
                        WHERE date >= DATE_SUB(CURDATE(), INTERVAL 6 DAY)
                        ORDER BY date DESC, shift_number ASC
                    ";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new ShiftAccumulation
                                {
                                    Id = (int)reader["id"],
                                    Date = ((DateTime)reader["date"]).Date,
                                    ShiftNumber = (int)reader["shift_number"],
                                    TotalSeconds = (int)reader["total_seconds"],
                                    UpdatedAt = (DateTime)reader["updated_at"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting last 7 days: {ex.Message}");
            }

            return result;
        }

        // RESET data untuk tanggal & shift tertentu (opsional)
        public void ResetAccumulation(DateTime date, int shiftNumber)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        UPDATE shift_accumulation_a
                        SET total_seconds = 0, updated_at = NOW()
                        WHERE date = @date AND shift_number = @shiftNumber
                    ";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@shiftNumber", shiftNumber);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error resetting accumulation: {ex.Message}");
            }
        }
    }

    public class ShiftAccumulation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ShiftNumber { get; set; }
        public int TotalSeconds { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
