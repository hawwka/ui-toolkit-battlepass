namespace Project.UI.BattlePass.Models
{
    public sealed class UserBattlePassState
    {
        public bool HasPremiumPass { get; }
        public int CurrentLevel { get; }

        public UserBattlePassState(bool hasPremiumPass, int currentLevel)
        {
            HasPremiumPass = hasPremiumPass;
            CurrentLevel = currentLevel;
        }
    }
}
