using System.Collections.Generic;
using Project.UI.BattlePass.Models;

namespace Project.UI.BattlePass.Providers
{
    public interface IBattlePassDataProvider
    {
        IReadOnlyList<BattlePassLevelModel> GetLevels();
    }
}
