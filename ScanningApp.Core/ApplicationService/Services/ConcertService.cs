using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ScanningApp.Core.ApplicationService.Services
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _concertRepo;

        public ConcertService(IConcertRepository concertRepository) => _concertRepo = concertRepository;

        public void CreateConcert(Concert concert)
        {
            _concertRepo.CreateConcert(concert);
        }

        public Concert FindConcertsById(int id)
        {
            var concert = _concertRepo.FindConcertById(id);
        
            return concert;
        }

        public List<Concert> GetAllConcerts()
        {
            return _concertRepo.GetAllConcerts().ToList();
        }

        public List<Concert> GetUpcomingConcerts()
        {
            return _concertRepo.GetUpcomingConcerts();
        }
    }
}
