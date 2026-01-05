using MySql.Data.MySqlClient;
using Dapper;
using CentralizedDashboard.Models;

namespace CentralizedDashboard.Services
{
    public class ShiftConfigurationService
    {
        private string connectionString = "Server=localhost;Port=3306;Database=monitoring_centralized;User=root;Password=;";

        public List<ShiftConfiguration> GetAllShifts()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM shift_configuration ORDER BY id";
                return connection.Query<ShiftConfiguration>(sql).ToList();
            }
        }

        public ShiftConfiguration? GetShiftByName(string shiftName)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM shift_configuration WHERE shift_name = @ShiftName";
                return connection.QueryFirstOrDefault<ShiftConfiguration>(sql, new { ShiftName = shiftName });
            }
        }

        public bool SaveShiftConfiguration(ShiftConfiguration shift)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Check if shift already exists
                    var existing = GetShiftByName(shift.ShiftName);
                    
                    if (existing != null)
                    {
                        // Update existing
                        string sql = @"UPDATE shift_configuration 
                                     SET start_time = @StartTime, 
                                         end_time = @EndTime, 
                                         updated_at = @UpdatedAt 
                                     WHERE shift_name = @ShiftName";
                        
                        connection.Execute(sql, new
                        {
                            StartTime = shift.StartTime.ToString("HH:mm:ss"),
                            EndTime = shift.EndTime.ToString("HH:mm:ss"),
                            UpdatedAt = DateTime.Now,
                            ShiftName = shift.ShiftName
                        });
                    }
                    else
                    {
                        // Insert new
                        string sql = @"INSERT INTO shift_configuration (shift_name, start_time, end_time, updated_at) 
                                     VALUES (@ShiftName, @StartTime, @EndTime, @UpdatedAt)";
                        
                        connection.Execute(sql, new
                        {
                            ShiftName = shift.ShiftName,
                            StartTime = shift.StartTime.ToString("HH:mm:ss"),
                            EndTime = shift.EndTime.ToString("HH:mm:ss"),
                            UpdatedAt = DateTime.Now
                        });
                    }
                    
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InitializeDefaultShifts()
        {
            try
            {
                var shifts = GetAllShifts();
                if (shifts.Count == 0)
                {
                    // Insert default shifts
                    SaveShiftConfiguration(new ShiftConfiguration
                    {
                        ShiftName = "Shift 1",
                        StartTime = new TimeOnly(7, 56),
                        EndTime = new TimeOnly(17, 0)
                    });

                    SaveShiftConfiguration(new ShiftConfiguration
                    {
                        ShiftName = "Shift 2",
                        StartTime = new TimeOnly(17, 1),
                        EndTime = new TimeOnly(0, 25)
                    });

                    SaveShiftConfiguration(new ShiftConfiguration
                    {
                        ShiftName = "Shift 3",
                        StartTime = new TimeOnly(0, 26),
                        EndTime = new TimeOnly(7, 55)
                    });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
