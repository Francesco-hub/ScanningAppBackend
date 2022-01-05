using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningApp.Core.ApplicationService
{
    public interface IUserService
    {
        //READ
        List<User> GetAllUsers();
    }
}
