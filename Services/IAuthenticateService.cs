using ClickerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Services
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(TokenRequest request, out string token);
        string CreateToken(string username);
    }
}
