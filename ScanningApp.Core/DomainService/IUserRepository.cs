using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanningApp.Core.DomainService
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
    }
}
