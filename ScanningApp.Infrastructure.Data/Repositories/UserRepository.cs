using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningApp.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ScanningAppContext _ctx;

        public UserRepository(ScanningAppContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<User> GetAllUsers()
        {   
                return _ctx.Users;
            
        }
    }
}
