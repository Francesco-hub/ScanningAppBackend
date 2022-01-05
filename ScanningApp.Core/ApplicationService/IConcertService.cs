using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningApp.Core.ApplicationService
{
    public interface IConcertService
    {
        //CREATE
        void CreateConcert(Concert concert);

        //READ
        Concert FindConcertsById(int id);
        List<Concert> GetAllConcerts();
        List<Concert> GetUpcomingConcerts();
    }
}
