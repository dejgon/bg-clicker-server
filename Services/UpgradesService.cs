using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ClickerAPI.Models;
using ClickerAPI.Models.Dao;

namespace ClickerAPI.Services
{
    public class UpgradesService
    {
        private readonly IMongoCollection<Upgrade> _upgrades;
        public UpgradesService(IClickerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _upgrades = database.GetCollection<Upgrade>(settings.UpgradesCollectionName);
        }

        public List<Upgrade> Get() =>
            _upgrades.Find(upgrade => true).ToList();

        public Upgrade Get(string id) =>
            _upgrades.Find<Upgrade>(upgrade => upgrade.Id == id).FirstOrDefault();

        public Upgrade Create(Upgrade upgrade)
        {
            _upgrades.InsertOne(upgrade);
            return upgrade;
        }
        public int Length()
        {
            return this.Get().Count();
        }
        public void Update(string id, Upgrade upgradeIn) =>
            _upgrades.ReplaceOne(upgrade => upgrade.Id == id, upgradeIn);

        public void Remove(UserDao upgradeIn) =>
            _upgrades.DeleteOne(upgrade => upgrade.Id == upgradeIn.Id);

        public void Remove(string id) =>
            _upgrades.DeleteOne(upgrade => upgrade.Id == id);
    }
}

