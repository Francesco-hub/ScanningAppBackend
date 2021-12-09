using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningApp.Core.DomainService
{
    public interface IScanRepository
    {
        //CreateScan Data
        //No id when enter, but id when exits
        Scan CreateScan(Scan scan);
        //Read Data
        Scan FindScanByConcertId(int id);
        IEnumerable<Scan> GetAllScans();
    }
}