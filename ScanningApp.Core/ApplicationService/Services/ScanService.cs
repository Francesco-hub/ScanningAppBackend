using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanningApp.Core.ApplicationService.Services
{
    public class ScanService : IScanService
    {
        readonly IScanRepository _scanRepo;
        readonly IConcertRepository _eventRepo;

        public ScanService(IScanRepository scanRepo)
        {
            _scanRepo = scanRepo;
        }

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