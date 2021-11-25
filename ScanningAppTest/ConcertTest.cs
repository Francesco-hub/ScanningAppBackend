using Moq;
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
        [Fact]
        public void TestFindConcertByIdIncludeScans() 
        {
            Mock<IConcertRepository> mockRepo = new Mock<IConcertRepository>();
            Concert concert1 = new Concert
            {
                Id = 1,
                Date = DateTime.Now,
                Time = DateTime.Now,
                Title = "TestConcert1",
                Scans = new List<Scan>()
            };
            Concert concert2 = new Concert
            {
                Id = 2,
                Date = DateTime.Now,
                Time = DateTime.Now,
                Title = "TestConcert2",
                Scans = new List<Scan>()
            };

        Concert[] concertList ={concert1,concert2};
                
            mockRepo.Setup(m => m.FindConcertByIdIncludeScans(1)).Returns(() => concertList[0]);
            ConcertService concertService = new ConcertService(mockRepo.Object);
            Assert.True(concertService.FindConcertByIdIncludeScans(1) == concert1);

        }

        [Fact]
        public void TestGetAllConcerts()
        {
            Mock<IConcertRepository> mockRepo = new Mock<IConcertRepository>();
            Concert[] concertList ={ new Concert
            {
                Id = 1,
                Date = DateTime.Now,
                Time = DateTime.Now,
                Title = "TestConcert1",
                Scans = new List<Scan>()
            },
                new Concert{
                Id = 2,
                Date = DateTime.Now,
                Time = DateTime.Now,
                Title = "TestConcert2",
                Scans = new List<Scan>()
                }

            };
            mockRepo.Setup(m => m.GetAllConcerts()).Returns(() => concertList.ToList());
            ConcertService concertService = new ConcertService(mockRepo.Object);
            Assert.True(concertService.GetAllConcerts().Count == 2);
        }

    }
}
