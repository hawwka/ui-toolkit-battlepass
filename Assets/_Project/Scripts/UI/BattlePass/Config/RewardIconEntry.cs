using System;
using UnityEngine;

namespace Project.UI.BattlePass.Config
{
    [Serializable]
    public sealed class RewardIconEntry
    {
        [field: SerializeField] public string AssetKey { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
