using System;
using System.Collections.Generic;
using Project.UI.BattlePass.Config;
using Project.UI.BattlePass.Providers;
using Project.UI.BattlePass.ViewModels;
using Project.UI.Navigation;
using UnityEngine.UIElements;

namespace Project.UI.BattlePass
{
    public sealed class BattlePassPanelView : IPanelView
    {
        private const int VisibleLevelCount = 10;

        private readonly ScrollView _scrollView;
        private readonly VisualElement _levelsRow;
        private readonly VisualTreeAsset _levelItemTemplate;
        private readonly IRewardIconProvider _iconProvider;
        private readonly List<VisualElement> _levelItems = new();

        private HorizontalScrollInputAdapter _scrollInputAdapter;
        private EventCallback<GeometryChangedEvent> _geometryChangedCallback;

        public BattlePassPanelView(BattlePassPanelConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (config.Uxml == null)
                throw new ArgumentException($"Panel config '{config.Id}' has no UXML assigned.", nameof(config));

            if (config.LevelItemTemplate == null)
                throw new ArgumentException($"Panel config '{config.Id}' has no level item template assigned.", nameof(config));

            _levelItemTemplate = config.LevelItemTemplate;
            _iconProvider = new SerializedRewardIconProvider(config.IconEntries);

            Root = config.Uxml.Instantiate();
            Root.AddToClassList("battle-pass-panel-root");

            _scrollView = Root.Q<ScrollView>("levels-scroll");
            _levelsRow = Root.Q<VisualElement>("levels-row");

            if (_scrollView != null)
            {
                _scrollView.mode = ScrollViewMode.Horizontal;
                _scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
                _scrollInputAdapter = new HorizontalScrollInputAdapter(_scrollView);
                _geometryChangedCallback = OnScrollGeometryChanged;
                _scrollView.RegisterCallback(_geometryChangedCallback);
            }

            PanelCloseButtonBinder.Bind(Root, OnCloseClicked);

            var dataProvider = new ScriptableObjectBattlePassDataProvider(config.BattlePassConfig);
            var stateProvider = new ScriptableObjectUserBattlePassStateProvider(config.UserStateConfig);
            Bind(new BattlePassPanelViewModel(dataProvider, stateProvider));
        }

        public VisualElement Root { get; }
        public event Action CloseRequested;

        public void Bind(object viewModel)
        {
            if (viewModel is not BattlePassPanelViewModel panelViewModel)
                return;

            BuildLevelItems(panelViewModel);
        }

        public void OnOpened() { }

        public void OnClosed()
        {
            if (_scrollView != null && _geometryChangedCallback != null)
                _scrollView.UnregisterCallback(_geometryChangedCallback);

            _scrollInputAdapter?.Dispose();
            _scrollInputAdapter = null;
        }

        private void BuildLevelItems(BattlePassPanelViewModel viewModel)
        {
            if (_levelsRow == null || _levelItemTemplate == null)
                return;

            _levelsRow.Clear();
            _levelItems.Clear();

            foreach (var levelViewModel in viewModel.Levels)
            {
                var container = _levelItemTemplate.Instantiate();
                var itemRoot = container.Q(className: "battle-pass-level-item") ?? container;
                itemRoot.AddToClassList("battle-pass-level-item");
                BattlePassLevelItemBinder.Bind(itemRoot, levelViewModel, _iconProvider);
                _levelsRow.Add(container);
                _levelItems.Add(itemRoot);
            }

            UpdateItemWidths();
        }

        private void OnScrollGeometryChanged(GeometryChangedEvent evt) => UpdateItemWidths();

        private void UpdateItemWidths()
        {
            if (_scrollView == null || _levelItems.Count == 0)
                return;

            var viewportWidth = _scrollView.contentViewport.resolvedStyle.width;
            if (float.IsNaN(viewportWidth) || viewportWidth <= 0f)
                return;

            var itemWidth = viewportWidth / VisibleLevelCount;
            foreach (var item in _levelItems)
                item.style.width = itemWidth;
        }

        private void OnCloseClicked() => CloseRequested?.Invoke();
    }
}
