namespace CentralizedDashboard.Models
{
    public class ShiftConfiguration
    {
        public int Id { get; set; }
        public string ShiftName { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
