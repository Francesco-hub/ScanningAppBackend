using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScanningApp.Core.ApplicationService
{
    public interface IScanService
    {

        //CreateScan //POST
        Scan CreateScans(List<Scan> scanList);

        //Read //GET
        Scan FindScanByConcertId(int concertId);
        List<Scan> GetAllScans();
    }
}