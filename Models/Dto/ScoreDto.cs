using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Models
{
    public class ScoreDto
    {
        public string Username { get; set; }
        public int Score { get; set; }

        public ScoreDto (string username, int score)
        {
            this.Username = username;
            this.Score = score;
        }
    }
}
