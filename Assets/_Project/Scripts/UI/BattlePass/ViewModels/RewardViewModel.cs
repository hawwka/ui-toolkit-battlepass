using Project.UI.BattlePass.Models;

namespace Project.UI.BattlePass.ViewModels
{
    public sealed class RewardViewModel
    {
        public string Id { get; }
        public string Title { get; }
        public string IconAssetKey { get; }
        public int Amount { get; }

        public RewardViewModel(RewardModel model)
        {
            Id = model.Id;
            Title = model.Title;
            IconAssetKey = model.IconAssetKey;
            Amount = model.Amount;
        }
    }
}
