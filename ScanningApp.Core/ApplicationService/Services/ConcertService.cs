﻿using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanningApp.Core.ApplicationService.Services
{
    public class ConcertService : IConcertService
    {
        readonly IConcertRepository _concertRepo;
        readonly IScanRepository _scanRepo;

        public ConcertService(IConcertRepository concertRepository)
            //, IScanRepository scanRepository)
        {
            _concertRepo = concertRepository;
            //_scanRepo = scanRepository;
        }

       /* public Concert FindConcertById(int id)
        {
            return _concertRepo.FindConcertById(id);
        }*/

        public Concert FindConcertByIdIncludeScans(int id)
        {
            var concert = _concertRepo.FindConcertByIdIncludeScans(id);
        
            return concert;
        }

        public List<Concert> GetAllConcerts()
        {
            return _concertRepo.GetAllConcerts().ToList();
        }

    }
}