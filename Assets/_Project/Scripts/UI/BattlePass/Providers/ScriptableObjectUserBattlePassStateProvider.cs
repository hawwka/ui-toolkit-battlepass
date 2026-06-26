using Project.UI.BattlePass.Config;
using Project.UI.BattlePass.Models;

namespace Project.UI.BattlePass.Providers
{
    public sealed class ScriptableObjectUserBattlePassStateProvider : IUserBattlePassStateProvider
    {
        private readonly BattlePassUserStateConfig _config;

        public ScriptableObjectUserBattlePassStateProvider(BattlePassUserStateConfig config)
        {
            _config = config;
        }

        public UserBattlePassState GetState()
        {
            if (_config == null)
                return new UserBattlePassState(false, 0);

            return new UserBattlePassState(_config.HasPremiumPass, _config.CurrentLevel);
        }
    }
}
