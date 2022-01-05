using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningApp.Core.ApplicationService
{
    public interface IScanService
    {
        //POST
        Scan CreateScans(List<Scan> scanList);

        //READ
        Scan FindScanByConcertId(int concertId);
        List<Scan> GetAllScans();
    }
}