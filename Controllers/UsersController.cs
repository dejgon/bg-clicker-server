using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickerAPI.Models;
using ClickerAPI.Models.Dao;
using ClickerAPI.Models.Dto;
using ClickerAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClickerAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("My Policy")]
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private readonly UpgradesService _upgradesService;
        private readonly IAuthenticateService _authService;

        public UsersController(UserService userService, UpgradesService upgradesService, IAuthenticateService authService)
        {
            _userService = userService;
            _upgradesService = upgradesService;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token;
            if (_authService.IsAuthenticated(request, out token))
            {
                return Ok(token);
            }
            return BadRequest("Invalid Request");
        }

        [AllowAnonymous]
        [HttpPost, Route("register")]
        public IActionResult Register([FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _userService.Create(user);
            return Ok("Done");
        }

    }
}