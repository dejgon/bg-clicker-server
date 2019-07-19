using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Models
{
    public class UpgradeLvlsDao
    {
        public int UpgradeId { get; set; }
        public int UpgradeLvl { get; set; }

        public UpgradeLvlsDao(int upgradeId, int upgradeLvl)
        {
            UpgradeId = upgradeId;
            UpgradeLvl = upgradeLvl;
        }
    }

}
