﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanningApp.Core.Entity
{
    public class ConcertDTO
    {
        public int id { get; set; }

        public string title { get; set; }

        public string date { get; set; } /// <summary>
        /// kitar de aki y de ee
        /// </summary>

        public string start_date { get; set; }
    }
}