using System;
using Project.UI.BattlePass.Models;
using UnityEngine;

namespace Project.UI.BattlePass.Config
{
    [Serializable]
    public sealed class RewardConfig
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string IconAssetKey { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }

        public RewardModel ToModel() => new(Id, Title, IconAssetKey, Amount);
    }
}
