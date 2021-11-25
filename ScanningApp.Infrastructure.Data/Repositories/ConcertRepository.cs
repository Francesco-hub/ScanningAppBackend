﻿using Microsoft.EntityFrameworkCore;
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
        public Concert FindConcertById(int id)
        {
            return _ctx.Concerts.FirstOrDefault(c => c.Id == id);
        }

        public Concert FindConcertByIdIncludeScans(int id)
        {
            /* concert.Scans = _scanRepo.GetAllScans()
                 .Where(scan => scan.ConcertId == concert.Id)
                 .ToList();*/
            return _ctx.Concerts
                 .Include(c => c.Scans)
                 .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Concert> GetAllConcerts()
        {
            return _ctx.Concerts
                .Include(c => c.Scans);
        }
    }
}