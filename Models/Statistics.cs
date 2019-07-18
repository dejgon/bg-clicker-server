using ClickerAPI.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Models
{
    public class Statistics
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        public int Score { get; set; }
        public int Money { get; set; }
        public int PointsPerClick { get; set; }
        public int PointsPerSecond { get; set; }
        public int Clicks { get; set; }
        public int ScoreFromClicks { get; set; }
        public int ScoreFromSecond { get; set; }
        public List<UpgradeLvls> UpgradeLvls { get; set; }

   

        public Statistics(string username)
        {
            Username = username;
            Score = 0;
            Money = 0;
            PointsPerClick = 1;
            PointsPerSecond = 0;
            Clicks = 0;
            ScoreFromClicks = 0;
            ScoreFromSecond = 0;
            UpgradeLvls = new List<UpgradeLvls> { };
        }
    }
}
