using System;
using System.Collections.Generic;
using System.Text;

namespace ScanningApp.Core.Entity
{
    public class Concert
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public List<Scan> Scans { get; set; }

        //public int Image { get; set; }
    }
}
