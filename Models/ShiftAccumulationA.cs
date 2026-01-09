using System;

namespace CentralizedDashboard.Models
{
    public class ShiftAccumulationA
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ShiftNumber { get; set; }
        public int TotalSeconds { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
