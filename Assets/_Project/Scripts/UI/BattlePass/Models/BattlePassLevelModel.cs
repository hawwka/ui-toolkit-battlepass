namespace Project.UI.BattlePass.Models
{
    public sealed class BattlePassLevelModel
    {
        public int Level { get; }
        public RewardModel FreeReward { get; }
        public RewardModel PremiumReward { get; }

        public BattlePassLevelModel(int level, RewardModel freeReward, RewardModel premiumReward)
        {
            Level = level;
            FreeReward = freeReward;
            PremiumReward = premiumReward;
        }
    }
}
