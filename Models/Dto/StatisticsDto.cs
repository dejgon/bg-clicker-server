using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Models.Dto
{
    public class StatisticsDto
    {
        public int Score { get; set; }
        public int Money { get; set; }
        public int PointsPerClick { get; set; }
        public int PointsPerSecond { get; set; }
        public int Clicks { get; set; }
        public int ScoreFromClicks { get; set; }
        public int ScoreFromSecond { get; set; }
        public int TimePlayed { get; set; }
        public List<UpgradeLvlsDto> UpgradeLevels { get; set; }
    }
}
