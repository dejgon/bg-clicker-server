using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickerAPI.Models.Dao;
using ClickerAPI.Models.Dto;
using ClickerAPI.Services;
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

        public UsersController(UserService userService, UpgradesService upgradesService)
        {
            _userService = userService;
            _upgradesService = upgradesService;
        }

        /// <summary>
        /// Returns a list of all users existing in database
        /// </summary>
        /// <returns>List of all users</returns>
        /// <response code="200">Returns list of all users</response>
        /// <response code="500">If any error occurs</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<List<UserDao>> Get() =>
            _userService.Get();

        [HttpGet, Route("stats")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<List<StatisticsDto>> GetStats()
        {
            var users = _userService.Get();
            List<StatisticsDao> stats = new List<StatisticsDao>();
            foreach(var item in users){
                stats.Add(item.Statistics);
            }
            return Ok(stats);
        }

    }
}
