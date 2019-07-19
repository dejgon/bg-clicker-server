using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Models
{
    public class UpgradeLvl
    {
        public int Id { get; set; }
        public int Level { get; set; }

        public UpgradeLvl(int upgradeId, int upgradeLvl)
        {
            Id = upgradeId;
            Level = upgradeLvl;
        }
    }

}
