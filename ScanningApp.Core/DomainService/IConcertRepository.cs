using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningApp.Core.DomainService
{
    public interface IConcertRepository
    {
        //Read Data
        Concert FindConcertById(int id);
        IEnumerable<Concert> GetAllConcerts();
        Concert FindConcertByIdIncludeScans(int id);
    }
}
