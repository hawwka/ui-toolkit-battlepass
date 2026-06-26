using System;
using UnityEngine.UIElements;

namespace Project.UI.Navigation
{
    public static class PanelCloseButtonBinder
    {
        public static Button Bind(VisualElement root, Action onClose)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));

            var closeButton = root.Q<Button>("close-button");
            if (closeButton != null)
                closeButton.clicked += () => onClose?.Invoke();

            return closeButton;
        }
    }
}
