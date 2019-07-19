using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClickerAPI.Models
{
    
    public class Upgrade
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        public int PointsPerClick { get; set; }
        public int PointsPerSecond { get; set; }
        public int Cost { get; set; }

    }
    
}
