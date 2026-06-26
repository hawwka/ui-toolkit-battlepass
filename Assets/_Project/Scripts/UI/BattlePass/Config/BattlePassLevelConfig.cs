using System;
using Project.UI.BattlePass.Models;
using UnityEngine;

namespace Project.UI.BattlePass.Config
{
    [Serializable]
    public sealed class BattlePassLevelConfig
    {
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public RewardConfig FreeReward { get; private set; }
        [field: SerializeField] public RewardConfig PremiumReward { get; private set; }

        public BattlePassLevelModel ToModel()
        {
            return new BattlePassLevelModel(
                Level,
                FreeReward.ToModel(),
                PremiumReward.ToModel());
        }
    }
}
