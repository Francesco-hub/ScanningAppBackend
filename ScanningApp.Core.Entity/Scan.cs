namespace ScanningApp.Core.Entity
{
    public class Scan
    {
        public int Id { get; set; }
        // public Concert Concert { get; set; }
        public int ConcertId { get; set; }
        public string SecurityCode { get; set; }

        //public int ScanTime { get; set; }
    }
}
