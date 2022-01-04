using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanningApp.Infrastructure.Data.Repositories
{
    public class ScanRepository : IScanRepository
    {
        readonly ScanningAppContext _ctx;

        public ScanRepository(ScanningAppContext ctx)
        {
            _ctx = ctx;
        }
        public Scan CreateScans(List<Scan> scanList)
        {
            var myScan = new Scan();
            //We have SQL database taking care of id's
            foreach (Scan scn in scanList)
            {
                myScan = _ctx.Scans.Add(scn).Entity;
                _ctx.SaveChanges();
            }
            
            return myScan;
        }

        /*public Scan CreateScans(Scan scan)
        {
            var myScan = new Scan();
            myScan = _ctx.Scans.Add(scan).Entity;
            _ctx.SaveChanges();
            return myScan;
        }*/

        public Scan FindScanByConcertId(int id)
        {
            return _ctx.Scans.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Scan> GetAllScans()
        {
            return _ctx.Scans;
        }
    }
}
