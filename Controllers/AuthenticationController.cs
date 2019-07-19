using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickerAPI.Models;
using ClickerAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClickerAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateService _authService;
        public AuthenticateController(IAuthenticateService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost, Route("request")]
        public IActionResult RequestToken([FromBody] TokenRequest request)
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet, Route("check")]
        public ActionResult testFunction()
        {
            return Ok(new { Message = "authenticated" });
        }
    }
}