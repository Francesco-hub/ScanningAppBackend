using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;

namespace ScanningApp.Core.DomainService
{
    public interface IConcertRepository
    {
        //CREATE
        void CreateConcert(Concert concert);

        //Read Data
        Concert FindConcertById(int id);
        IEnumerable<Concert> GetAllConcerts();
        Concert FindConcertByIdIncludeScans(int id);

        List<Concert> GetUpcomingConcerts(DateTime date);
    }
}
