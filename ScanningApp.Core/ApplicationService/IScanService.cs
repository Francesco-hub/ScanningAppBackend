using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScanningApp.Core.ApplicationService
{
    public interface IScanService
    {

        //CreateScan //POST
        Scan CreateScan(Scan scan);

        //Read //GET
        Scan FindScanByConcertId(int concertId);
        List<Scan> GetAllScans();
    }
}