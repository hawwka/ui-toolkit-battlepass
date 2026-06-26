using System;
using UnityEngine.UIElements;

namespace Project.UI.Navigation
{
    public sealed class ModalOverlayView
    {
        private readonly VisualElement _root;
        private readonly VisualElement _backdrop;
        private readonly VisualElement _blurLayer;
        private readonly VisualElement _panelContainer;

        public ModalOverlayView(VisualElement overlayRoot)
        {
            _root = overlayRoot;

            _backdrop = new VisualElement { name = "modal-backdrop" };
            _backdrop.AddToClassList("modal-backdrop");
            _backdrop.RegisterCallback<ClickEvent>(_ => BackdropClicked?.Invoke());

            _blurLayer = new VisualElement { name = "modal-blur" };
            _blurLayer.AddToClassList("modal-blur");
            _blurLayer.pickingMode = PickingMode.Ignore;

            _panelContainer = new VisualElement { name = "panel-container" };
            _panelContainer.AddToClassList("panel-container");
            _panelContainer.pickingMode = PickingMode.Ignore;

            _root.Add(_backdrop);
            _root.Add(_blurLayer);
            _root.Add(_panelContainer);

            Hide();
        }

        public VisualElement Backdrop => _backdrop;
        public VisualElement BlurLayer => _blurLayer;
        public event Action BackdropClicked;

        public void SetPanel(VisualElement panelRoot)
        {
            _panelContainer.Clear();
            if (panelRoot != null)
                _panelContainer.Add(panelRoot);
        }

        public void Show()
        {
            _root.style.display = DisplayStyle.Flex;
            _root.BringToFront();
        }

        public void Hide()
        {
            _panelContainer.Clear();
            _root.style.display = DisplayStyle.None;
        }
    }
}
