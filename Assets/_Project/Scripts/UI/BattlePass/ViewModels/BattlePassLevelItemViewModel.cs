using Project.UI.BattlePass.Models;

namespace Project.UI.BattlePass.ViewModels
{
    public sealed class BattlePassLevelItemViewModel
    {
        public int Level { get; }
        public RewardViewModel FreeReward { get; }
        public RewardViewModel PremiumReward { get; }
        public bool IsFreeUnlocked { get; }
        public bool IsPremiumUnlocked { get; }

        public BattlePassLevelItemViewModel(BattlePassLevelModel level, int currentLevel, bool hasPremiumPass)
        {
            Level = level.Level;
            FreeReward = new RewardViewModel(level.FreeReward);
            PremiumReward = new RewardViewModel(level.PremiumReward);
            IsFreeUnlocked = currentLevel >= level.Level;
            IsPremiumUnlocked = hasPremiumPass && currentLevel >= level.Level;
        }
    }
}
