using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScanningApp.Core.ApplicationService
{
    public interface IConcertService
    {
        //CREATE
        void CreateConcert(Concert concert);

        //Read //GET
        //Concert FindConcertById(int id);
        Concert FindConcertByIdIncludeScans(int id);
        List<Concert> GetAllConcerts();

        List<Concert> GetUpcomingConcerts(DateTime date);
    }
}
