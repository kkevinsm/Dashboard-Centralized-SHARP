using System;

namespace CentralizedDashboard.Models 
{
    public class ErrorLog
    {
        public int id { get; set; }
        public DateTime waktu { get; set; }
        public string nama_mesin { get; set; }
        public int kode_error { get; set; }
    }
}