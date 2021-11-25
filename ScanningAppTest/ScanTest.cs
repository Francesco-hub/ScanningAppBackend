using Moq;
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
                ConcertId = 3
            },new Scan
            {
                Id = 2,
                SecurityCode = "gy8H0pBmS",
                ConcertId = 2
            }
            };
            mockRepo.Setup(m => m.GetAllScans()).Returns(() => scanList.ToList());
            ScanService scanService = new ScanService(mockRepo.Object);
            Assert.True(scanService.GetAllScans().Count == 2);
        }
        [Fact]
        public void TestCreateScan()
        {
            /* Mock<IScanRepository> mockRepo = new Mock<IScanRepository>();
             Scan newScan = new Scan
             {
                 Id = 2,
                 SecurityCode = "gy8H0pBmS",
                 ConcertId = 3
             };
             List<Scan> scanList = new List<Scan>{
                 new Scan
             {
                 Id = 1,
                 SecurityCode = "q75HB34s",
                 ConcertId = 3
             }
             };

             mockRepo.Setup(m => m.GetAllScans()).Returns(() => scanList.ToList());
             mockRepo.As<List<Scan>>().Setup(m => m.Add(newScan)).Callback<Scan>(newScan => scanList.Add(newScan));
             ScanService scanService = new ScanService(mockRepo.Object);
             scanList.Add(newScan);
            */

        }
        [Fact]
        public void TestFindScanByConcertId()
        {
            Mock<IScanRepository> mockRepo = new Mock<IScanRepository>();
            Scan[] scanList ={ new Scan
            {
                Id = 1,
                SecurityCode = "q75HB34s",
                ConcertId = 3
            },new Scan
            {
                Id = 2,
                SecurityCode = "gy8H0pBmS",
                ConcertId = 2
            }
            };

            mockRepo.Setup(m => m.FindScanByConcertId(2)).Returns(() => scanList[1]);
            ScanService scanService = new ScanService(mockRepo.Object);
            Assert.True(scanService.FindScanByConcertId(2).SecurityCode == "gy8H0pBmS");
        }
    }
}
