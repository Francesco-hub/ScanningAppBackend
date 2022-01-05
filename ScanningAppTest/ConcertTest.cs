using Moq;
using ScanningApp.Core.ApplicationService;
using ScanningApp.Core.ApplicationService.Services;
using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using ScanningApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ScanningAppTest
{
    public class ConcertTest

    {
       /* public List<Concert> GetUpcomingConcerts()
        {
            throw new NotImplementedException();
        }*/

        [Fact]
        public void TestFindConcertById()
        {
            Mock<IConcertRepository> mockRepo = new Mock<IConcertRepository>();

            Concert concert1 = new Concert
            {
                id = 1,
                start_date = DateTime.Now,
                title = "TestConcert1"
            };
            Concert concert2 = new Concert
            {
                id = 2,
                start_date = DateTime.Now,
                title = "TestConcert2"
            };
            Concert[] concertList = { concert1, concert2 };


            mockRepo.Setup(m => m.FindConcertById(1)).Returns(() => concertList[0]);
             ConcertService concertService = new ConcertService(mockRepo.Object);
             Assert.True(concertService.FindConcertsById(1) == concert1);
            
            Assert.True(true);
        }

        [Fact]
        public void TestGetAllConcerts()
        {
            Mock<IConcertRepository> mockRepo = new Mock<IConcertRepository>();
            Concert concert1 = new Concert
            {
                id = 1,
                start_date = DateTime.Now,
                title = "TestConcert1"
            };
            Concert concert2 = new Concert
            {
                id = 2,
                start_date = DateTime.Now,
                title = "TestConcert2"
            };
            Concert[] concertList = { concert1, concert2 };
            mockRepo.Setup(m => m.GetAllConcerts()).Returns(() => concertList.ToList());
             ConcertService concertService = new ConcertService(mockRepo.Object);
             Assert.True(concertService.GetAllConcerts().Count == 2);
            Assert.True(true);
        }
        [Fact]
        public void TestCreateConcert()
        {
            Mock<IConcertRepository> mockRepo = new Mock<IConcertRepository>();
            Concert concert1 = new Concert
            {
                id = 1,
                start_date = DateTime.Now,
                title = "TestConcert1"
            };
            Concert concert2 = new Concert
            {
                id = 2,
                start_date = DateTime.Now,
                title = "TestConcert2"
            };
            Concert[] concertList = { concert1, concert2 };
            ConcertService concertService = new ConcertService(mockRepo.Object);
            concertService.CreateConcert(concert1);
            mockRepo.Verify(r => r.CreateConcert(concert1), Times.Once);

        }

        
        
    }
}
