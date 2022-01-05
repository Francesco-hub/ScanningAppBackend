using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningApp.Core.DomainService
{
    public interface IScanRepository
    {
        //CREATE
        Scan CreateScans(List<Scan> scanList);

        //READ
        Scan FindScanByConcertId(int id);
        IEnumerable<Scan> GetAllScans();
    }
}