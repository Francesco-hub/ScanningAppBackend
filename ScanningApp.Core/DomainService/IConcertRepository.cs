using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningApp.Core.DomainService
{
    public interface IConcertRepository
    {
        //CREATE
        void CreateConcert(Concert concert);

        //READ
        Concert FindConcertById(int id);
        IEnumerable<Concert> GetAllConcerts();
        List<Concert> GetUpcomingConcerts();
    }
}
