using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickerAPI.Models;
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
        private readonly StatisticsService _statsService;
        private readonly UpgradesService _upgradesService;

        public UsersController(UserService userService, StatisticsService statsService, UpgradesService upgradesService)
        {
            _userService = userService;
            _statsService = statsService;
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
        public ActionResult<List<User>> Get() =>
            _userService.Get();



        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        [EnableCors("My Policy")]
        public ActionResult<User> Create([FromBody] User user)
        {
            User userToDatabase;
            Statistics statistics;
            if (_userService.GetByUsername(user.Username) == null)
            {
                UpgradeLvls upgrade;
                var count = _upgradesService.Get().Count();
                statistics = new Statistics(user.Username);
                for (var i = 0; i < count; i++)
                {
                    upgrade = new UpgradeLvls(i, 0);
                    statistics.UpgradeLvls.Add(upgrade);
                }
                _statsService.Create(statistics);
                userToDatabase = new User(user.Username, user.Password);
                _userService.Create(userToDatabase);
            }
            else
            {
                return StatusCode(409, "User exists in database!");
            }
           
            return CreatedAtRoute("GetUser", new { id = userToDatabase.Id.ToString() }, userToDatabase);
        }

        [Route("login")]
        [HttpPost]
        [EnableCors("My Policy")]
        public ActionResult LoginReq([FromBody] User login)
        {
            Console.WriteLine(login.Username + login.Password);
            if (_userService.GetByUsername(login.Username) == null)
            {
                return StatusCode(404, "User doesn't exists!");
            }
            User user = _userService.GetByUsername(login.Username);
            if(user.Password != login.Password)
            {
                return StatusCode(401, "Wrong password or username");
            }
            user.Password = null;
            Statistics stats = _statsService.GetByUsername(user.Username);
            return Ok(new { User = user, Stats = stats});
        }




        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User userIn)
        {
            var user = _userService.Get(id);

            if(user == null)
            {
                return NotFound();
            }

            _userService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.Id);

            return NoContent();
        }


    }
}
