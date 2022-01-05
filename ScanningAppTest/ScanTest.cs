using Moq;
using ScanningApp.Core.ApplicationService;
using ScanningApp.Core.ApplicationService.Services;
using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScanningAppTest
{
    public class ScanTest 
    {
        [Fact]
        public void TestGetAllScans()
        {
            Mock<IScanRepository> mockRepo = new Mock<IScanRepository>();
            Scan[] scanList ={ new Scan
            {
                Id = 1,
                SecurityCode = "q75HB34s",
                ConcertId = 3,
                UserId = 1
            },new Scan
            {
                Id = 2,
                SecurityCode = "gy8H0pBmS",
                ConcertId = 2,
                UserId = 1
            }
            };
            mockRepo.Setup(m => m.GetAllScans()).Returns(() => scanList.ToList());
            ScanService scanService = new ScanService(mockRepo.Object);
            Assert.True(scanService.GetAllScans().Count == 2);
        }
        [Fact]
        public void TestFindScanByConcertId()
        {
            Mock<IScanRepository> mockRepo = new Mock<IScanRepository>();
            Scan[] scanList ={ new Scan
            {
                Id = 1,
                SecurityCode = "q75HB34s",
                ConcertId = 3,
                UserId = 1
            },new Scan
            {
                Id = 2,
                SecurityCode = "gy8H0pBmS",
                ConcertId = 2,
                UserId = 1
            }
            };
            mockRepo.Setup(m => m.FindScanByConcertId(2)).Returns(() => scanList[1]);
            ScanService scanService = new ScanService(mockRepo.Object);
            Assert.True(scanService.FindScanByConcertId(2).SecurityCode == "gy8H0pBmS");
        }
        [Fact]
        public void TestCreateScans()
        {
            Mock<IScanRepository> mockRepo = new Mock<IScanRepository>();
            Scan[] scanList ={ new Scan
            {
                Id = 1,
                SecurityCode = "q75HB34s",
                ConcertId = 3,
                UserId = 1
            },new Scan
            {
                Id = 2,
                SecurityCode = "gy8H0pBmS",
                ConcertId = 2,
                UserId = 1
            }
            };
            var scanAsList = scanList.ToList();
            
            ScanService scanService = new ScanService(mockRepo.Object);
            scanService.CreateScans(scanAsList);
            mockRepo.Verify(r => r.CreateScans(scanAsList), Times.Once);
        }


    }
}
