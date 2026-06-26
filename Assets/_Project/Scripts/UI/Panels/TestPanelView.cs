using System;
using System.Collections.Generic;
using Project.UI.Navigation;
using UnityEngine.UIElements;

namespace Project.UI.Panels
{
    public sealed class TestPanelView : IPanelView
    {
        private readonly Label _titleLabel;
        private readonly Label _contentLabel;

        public TestPanelView(PanelConfig config)
        {
            if (config.Uxml == null)
                throw new ArgumentException($"Panel config '{config.Id}' has no UXML assigned.", nameof(config));

            Root = config.Uxml.Instantiate();
            Root.AddToClassList("test-panel-root");

            _titleLabel = Root.Q<Label>("title-label");
            _contentLabel = Root.Q<Label>("content-label");
            PanelCloseButtonBinder.Bind(Root, OnCloseClicked);

            Bind(new TestPanelViewModel(config.Title, $"Content for panel: {config.Id}"));
        }

        public VisualElement Root { get; }
        public event Action CloseRequested;

        public void Bind(object viewModel)
        {
            if (viewModel is not TestPanelViewModel panelViewModel)
                return;

            if (_titleLabel != null)
                _titleLabel.text = panelViewModel.Title;

            if (_contentLabel != null)
                _contentLabel.text = panelViewModel.Content;
        }

        public void OnOpened() { }

        public void OnClosed() { }

        private void OnCloseClicked() => CloseRequested?.Invoke();
    }
}
