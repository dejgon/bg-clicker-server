using ClickerAPI.Models;
using ClickerAPI.Models.Dao;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Services
{
    public class UserManagementService: IUserManagementService
    {
        private readonly UserService _userService;

        public UserManagementService(UserService userService)
        {
            _userService = userService;
        }
        public bool IsValidUser(string username, string password)
        {
           return _userService.CheckIfValid(username, password);
        }
    }
}
