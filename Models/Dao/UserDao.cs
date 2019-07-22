using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Models.Dao
{
    public class UserDao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public StatisticsDao Statistics { get; set; }

        public StatisticsDao getStats()
        {
            return this.Statistics;
        }
    }
}
