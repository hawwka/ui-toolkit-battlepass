using UnityEngine;

namespace Project.UI.BattlePass.Config
{
    [CreateAssetMenu(fileName = "BattlePassUserStateConfig", menuName = "Project/Battle Pass/User State Config")]
    public sealed class BattlePassUserStateConfig : ScriptableObject
    {
        [field: SerializeField] public bool HasPremiumPass { get; private set; }
        [field: SerializeField] public int CurrentLevel { get; private set; }
    }
}
