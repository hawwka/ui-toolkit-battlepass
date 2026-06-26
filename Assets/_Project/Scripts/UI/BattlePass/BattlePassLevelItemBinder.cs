using Project.UI.BattlePass.Providers;
using Project.UI.BattlePass.ViewModels;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.UI.BattlePass
{
    public static class BattlePassLevelItemBinder
    {
        public static void Bind(VisualElement itemRoot, BattlePassLevelItemViewModel viewModel, IRewardIconProvider iconProvider)
        {
            var levelLabel = itemRoot.Q<Label>("level-label");
            if (levelLabel != null)
                levelLabel.text = viewModel.Level.ToString();

            BindReward(itemRoot.Q<VisualElement>("free-reward"), viewModel.FreeReward, viewModel.IsFreeUnlocked, iconProvider);
            BindReward(itemRoot.Q<VisualElement>("premium-reward"), viewModel.PremiumReward, viewModel.IsPremiumUnlocked, iconProvider);
        }

        private static void BindReward(
            VisualElement rewardRoot,
            RewardViewModel reward,
            bool isUnlocked,
            IRewardIconProvider iconProvider)
        {
            if (rewardRoot == null || reward == null)
                return;

            rewardRoot.EnableInClassList("battle-pass-reward--unlocked", isUnlocked);
            rewardRoot.EnableInClassList("battle-pass-reward--locked", !isUnlocked);

            var titleLabel = rewardRoot.Q<Label>("reward-title");
            if (titleLabel != null)
                titleLabel.text = reward.Title;

            var amountLabel = rewardRoot.Q<Label>("reward-amount");
            if (amountLabel != null)
                amountLabel.text = reward.Amount > 1 ? $"x{reward.Amount}" : string.Empty;

            var iconElement = rewardRoot.Q<VisualElement>("reward-icon");
            if (iconElement == null)
                return;

            var sprite = iconProvider.GetIcon(reward.IconAssetKey);
            iconElement.style.backgroundImage = sprite != null
                ? new StyleBackground(sprite)
                : StyleKeyword.Null;
        }
    }
}
