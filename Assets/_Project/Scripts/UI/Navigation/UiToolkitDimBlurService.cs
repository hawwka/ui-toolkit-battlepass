using UnityEngine.UIElements;

namespace Project.UI.Navigation
{
    public sealed class UiToolkitDimBlurService : IBackgroundBlurService
    {
        private const string ActiveClass = "modal-blur--active";

        private readonly VisualElement _blurLayer;
        private readonly VisualElement _backdrop;

        public UiToolkitDimBlurService(VisualElement blurLayer, VisualElement backdrop)
        {
            _blurLayer = blurLayer;
            _backdrop = backdrop;
        }

        public void Enable()
        {
            _blurLayer.AddToClassList(ActiveClass);
            _backdrop.AddToClassList(ActiveClass);
        }

        public void Disable()
        {
            _blurLayer.RemoveFromClassList(ActiveClass);
            _backdrop.RemoveFromClassList(ActiveClass);
        }
    }
}
