using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ScanningApp.Core.ApplicationService.Services
{
    public class ScanService : IScanService
    {
        private readonly IScanRepository _scanRepo;

        public ScanService(IScanRepository scanRepo) => _scanRepo = scanRepo;

        public Scan CreateScans(List<Scan> scanList)
        {
            return _scanRepo.CreateScans(scanList);
        }

        public Scan FindScanByConcertId(int id)
        {
            return _scanRepo.FindScanByConcertId(id);
        }

        public List<Scan> GetAllScans()
        {
            return _scanRepo.GetAllScans().ToList();
        }
    }
}