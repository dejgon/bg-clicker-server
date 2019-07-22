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
                _userService.AddTokenToUser(request.Username, token);
                return Ok(new {Token = token });
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
            if (_userService.Create(user) == false)
            {
                return BadRequest("User already exists!");
            }else
            {
                return StatusCode(201);
            }
            
        }

        [Authorize]
        [HttpGet, Route("{username}")]
        public ActionResult<StatisticsDao> Get(string username)
        {
            UserDao user = _userService.GetByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            var token = Request.Headers["Authorization"];
            if (token != ("Bearer " + user.Token))
            {
                return Unauthorized();
            }
            user.Password = null;
            StatisticsDao stats = user.getStats();
            return stats;
        }

    }
}