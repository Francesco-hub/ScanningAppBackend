using Microsoft.EntityFrameworkCore;
using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanningApp.Infrastructure.Data.Repositories
{
    public class ConcertRepository : IConcertRepository
    {
        readonly ScanningAppContext _ctx;

        public ConcertRepository(ScanningAppContext ctx)
        {
            _ctx = ctx;
        }

        public void CreateConcert(Concert concert)
        {
            _ctx.Concerts.Add(concert);
            _ctx.SaveChanges();
        }

        public Concert FindConcertById(int id)
        {
            return _ctx.Concerts.FirstOrDefault(c => c.id == id);
        }

        public IEnumerable<Concert> GetAllConcerts()
        {
            return _ctx.Concerts;
            
            
        }

        public List<Concert> GetUpcomingConcerts()
        {
            //return _ctx.Concerts.Where(c => DateTime.Compare(c.start_date, date) >= 0).ToList();
            return null;
        }
    }
}
