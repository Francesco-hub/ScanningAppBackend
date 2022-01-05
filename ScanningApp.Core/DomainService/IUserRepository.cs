using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningApp.Core.DomainService
{
    public interface IUserRepository
    {
        //READ
        IEnumerable<User> GetAllUsers();
    }
}
