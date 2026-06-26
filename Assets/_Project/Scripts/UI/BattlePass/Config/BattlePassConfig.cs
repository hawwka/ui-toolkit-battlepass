using System.Collections.Generic;
using UnityEngine;

namespace Project.UI.BattlePass.Config
{
    [CreateAssetMenu(fileName = "BattlePassConfig", menuName = "Project/Battle Pass/Battle Pass Config")]
    public sealed class BattlePassConfig : ScriptableObject
    {
        [field: SerializeField] public List<BattlePassLevelConfig> Levels { get; private set; } = new();
    }
}
