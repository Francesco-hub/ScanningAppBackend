using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanningApp.Core.ApplicationService.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public List<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers().ToList();
        }
    }
}
