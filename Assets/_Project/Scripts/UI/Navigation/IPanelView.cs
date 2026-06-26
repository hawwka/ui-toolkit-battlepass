using System;
using UnityEngine.UIElements;

namespace Project.UI.Navigation
{
    public interface IPanelView
    {
        VisualElement Root { get; }
        event Action CloseRequested;
        void Bind(object viewModel);
        void OnOpened();
        void OnClosed();
    }
}
