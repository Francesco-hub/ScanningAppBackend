using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanningApp.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly ScanningAppContext _ctx;

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
