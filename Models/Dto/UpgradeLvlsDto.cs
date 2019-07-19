using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickerAPI.Models.Dto
{
    public class UpgradeLvlsDto
    {
        public int UpgradeId { get; set; }
        public int UpgradeLvl { get; set; }

        public UpgradeLvlsDto(int upgradeId, int upgradeLvl)
        {
            UpgradeId = upgradeId;
            UpgradeLvl = upgradeLvl;
        }
    }
}
