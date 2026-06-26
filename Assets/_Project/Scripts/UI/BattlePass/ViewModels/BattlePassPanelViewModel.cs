using System.Collections.Generic;
using System.Linq;
using Project.UI.BattlePass.Providers;

namespace Project.UI.BattlePass.ViewModels
{
    public sealed class BattlePassPanelViewModel
    {
        public IReadOnlyList<BattlePassLevelItemViewModel> Levels { get; }
        public bool HasPremiumPass { get; }
        public int CurrentLevel { get; }

        public BattlePassPanelViewModel(
            IBattlePassDataProvider dataProvider,
            IUserBattlePassStateProvider stateProvider)
        {
            var state = stateProvider.GetState();
            HasPremiumPass = state.HasPremiumPass;
            CurrentLevel = state.CurrentLevel;

            Levels = dataProvider.GetLevels()
                .Select(level => new BattlePassLevelItemViewModel(level, state.CurrentLevel, state.HasPremiumPass))
                .ToList();
        }
    }
}
