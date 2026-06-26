namespace Project.UI.BattlePass.Models
{
    public sealed class RewardModel
    {
        public string Id { get; }
        public string Title { get; }
        public string IconAssetKey { get; }
        public int Amount { get; }

        public RewardModel(string id, string title, string iconAssetKey, int amount)
        {
            Id = id;
            Title = title;
            IconAssetKey = iconAssetKey;
            Amount = amount;
        }
    }
}
