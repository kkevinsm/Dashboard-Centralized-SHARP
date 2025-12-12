using System;

namespace CentralizedDashboard.Models 
{
    public class ErrorLog
    {
        public int id { get; set; }
        public DateTime waktu { get; set; }
        public string nama_mesin_proses { get; set; }
        
        public int data_timer { get; set; }
    }
}