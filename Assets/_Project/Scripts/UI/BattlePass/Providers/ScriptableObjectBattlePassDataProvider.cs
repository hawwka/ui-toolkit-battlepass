using System.Collections.Generic;
using System.Linq;
using Project.UI.BattlePass.Config;
using Project.UI.BattlePass.Models;

namespace Project.UI.BattlePass.Providers
{
    public sealed class ScriptableObjectBattlePassDataProvider : IBattlePassDataProvider
    {
        private readonly BattlePassConfig _config;
        private IReadOnlyList<BattlePassLevelModel> _cachedLevels;

        public ScriptableObjectBattlePassDataProvider(BattlePassConfig config)
        {
            _config = config;
        }

        public IReadOnlyList<BattlePassLevelModel> GetLevels()
        {
            if (_cachedLevels != null)
                return _cachedLevels;

            if (_config == null || _config.Levels == null)
            {
                _cachedLevels = new List<BattlePassLevelModel>();
                return _cachedLevels;
            }

            _cachedLevels = _config.Levels
                .Where(level => level != null)
                .Select(level => level.ToModel())
                .OrderBy(level => level.Level)
                .ToList();

            return _cachedLevels;
        }
    }
}
