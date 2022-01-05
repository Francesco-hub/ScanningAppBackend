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
    public class UserTest
    {
        [Fact]
        public void TestGetAllUsers()
        {
            Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
            User[] userList ={ new User
            {
                Id = 1,
                Code = 1111,
                FirstName = "Pia",
                LastName = "Jørs"

            },new User
            {
                Id = 2,
                Code = 2222,
                FirstName = "Gabriella",
                LastName = "Bergman"
            }
            };
            mockRepo.Setup(m => m.GetAllUsers()).Returns(() => userList.ToList());
            UserService userService = new UserService(mockRepo.Object);
            Assert.True(userService.GetAllUsers().Count == 2);
        }
    }
}
