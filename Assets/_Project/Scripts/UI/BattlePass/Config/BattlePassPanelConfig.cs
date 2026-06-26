using System.Collections.Generic;
using Project.UI.Panels;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.UI.BattlePass.Config
{
    [CreateAssetMenu(fileName = "BattlePassPanelConfig", menuName = "Project/UI/Battle Pass Panel Config")]
    public sealed class BattlePassPanelConfig : PanelConfig
    {
        [field: SerializeField] public BattlePassConfig BattlePassConfig { get; private set; }
        [field: SerializeField] public BattlePassUserStateConfig UserStateConfig { get; private set; }
        [field: SerializeField] public VisualTreeAsset LevelItemTemplate { get; private set; }
        [field: SerializeField] public List<RewardIconEntry> IconEntries { get; private set; } = new();
    }
}
