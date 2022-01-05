namespace ScanningApp.Core.Entity
{
    public class Scan
    {
        public int Id { get; set; }
        public int ConcertId { get; set; }
        public int UserId { get; set; }
        public string SecurityCode { get; set; }
    }
}
