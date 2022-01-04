using System;
using System.Collections.Generic;
using System.Text;

namespace ScanningApp.Core.Entity
{
    public class Concert
    {
        public int id { get; set; }

        public string title { get; set; }

        public DateTime start_date { get; set; }

        //public List<Scan> Scans { get; set; }

        //public int Image { get; set; }
    }
}
