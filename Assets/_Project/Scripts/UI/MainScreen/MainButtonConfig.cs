using UnityEngine;

namespace Project.UI.MainScreen
{
    [CreateAssetMenu(fileName = "MainButtonConfig", menuName = "Project/UI/Main Button Config")]
    public sealed class MainButtonConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Label { get; private set; }
        [field: SerializeField] public string IconId { get; private set; }
        [field: SerializeField] public MainButtonPosition Position { get; private set; }
        [field: SerializeField] public MainButtonActionType ActionType { get; private set; }
        [field: SerializeField] public string PanelId { get; private set; }
    }
}
