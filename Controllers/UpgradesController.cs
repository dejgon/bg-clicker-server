using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickerAPI.Models;
using ClickerAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClickerAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("My Policy")]
    public class UpgradesController : Controller
    {
        private readonly UpgradesService _upgradesService;
        public UpgradesController(UpgradesService upgradesService)
        {
            _upgradesService = upgradesService;
        }
        [HttpGet]
        public ActionResult<List<Upgrade>> Get() =>
            _upgradesService.Get();
   
    [HttpGet("{id:length(24)}", Name = "GetUpgrade")]
    public ActionResult<Upgrade> Get(string id)
    {
        var upgrade = _upgradesService.Get(id);

        if (upgrade == null)
        {
            return NotFound();
        }

        return upgrade;
    }

    [HttpPost]
    public ActionResult<Upgrade> Create([FromBody] Upgrade upgrade)
    {
        _upgradesService.Create(upgrade);

        return CreatedAtRoute("GetUpgrade", new { id = upgrade.Id.ToString() }, upgrade);
    }

    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, Upgrade upgradeIn)
    {
        var upgrade = _upgradesService.Get(id);

        if (upgrade == null)
        {
            return NotFound();
        }

            _upgradesService.Update(id, upgradeIn);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var upgrade = _upgradesService.Get(id);

        if (upgrade == null)
        {
            return NotFound();
        }

            _upgradesService.Remove(upgrade.Id);

        return NoContent();
    }
}
}
