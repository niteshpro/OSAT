namespace OSAT.Models
{
    public class Log
    {
        public int id { get; set; }
        public string? Level { get; set; }  // e.g., Info, Warning, Error
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string? ApplicationName { get; set; }  // Optional: Source of the log
    }

}
