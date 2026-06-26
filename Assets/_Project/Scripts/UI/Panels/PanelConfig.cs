using UnityEngine;
using UnityEngine.UIElements;

namespace Project.UI.Panels
{
    [CreateAssetMenu(fileName = "PanelConfig", menuName = "Project/UI/Panel Config")]
    public class PanelConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public VisualTreeAsset Uxml { get; private set; }
    }
}
