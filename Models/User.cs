using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClickerAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        public string Password { get; set; }

        public Statistics Statistics { get; set; }
        public List<UserUpgrades> Upgrades { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            Statistics = new Statistics();
            Upgrades = new List<UserUpgrades> { };
        }
    }
    public class Statistics
    {
        public int Score { get; set; }
        public int Money { get; set; }
        public int PointsPerClick { get; set; }
        public int PointsPerSecond { get; set; }
        public int Clicks { get; set; }
        public int ScoreFromClicks { get; set; }
        public int ScoreFromSeconds { get; set; }

        public Statistics()
        {
            Score = 0;
            Money = 0;
            PointsPerClick = 1;
            PointsPerSecond = 0;
            Clicks = 0;
            ScoreFromClicks = 0;
            ScoreFromSeconds = 0;
        }

    }
    public class UserUpgrades
    {
        public int UpgradeId { get; set; }
        public int UpgradeLvl { get; set; }

       
    }
}
