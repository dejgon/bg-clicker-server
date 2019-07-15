using ClickerAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Services
{
    public class StatisticsService
    {
        private readonly IMongoCollection<Statistics> _stats;
        public StatisticsService(IClickerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _stats = database.GetCollection<Statistics>(settings.StatisticsCollectionName);
        }
        public List<Statistics> Get() =>
            _stats.Find(stat => true).ToList();
        public Statistics Get(string id) =>
            _stats.Find<Statistics>(stats => stats.Id == id).FirstOrDefault();
        public Statistics GetByUsername(string username) =>
            _stats.Find<Statistics>(stats => stats.Username == username).FirstOrDefault();

        public void Update(string id, Statistics statsIn)
        {
            Console.WriteLine(id);
            _stats.ReplaceOne(stats => stats.Id == id, statsIn);
        }

        public Statistics Create(Statistics stats)
        {
            _stats.InsertOne(stats);
            return stats;
        }
    }
}
