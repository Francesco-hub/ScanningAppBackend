using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ScanningApp.Core.ApplicationService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo) => _userRepo = userRepo;

        public List<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers().ToList();
        }
    }
}
