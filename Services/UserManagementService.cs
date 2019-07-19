using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Services
{
    public class UserManagementService: IUserManagementService
    {
        public bool IsValidUser(string username, string password)
        {
            return true;
        }
    }
}
