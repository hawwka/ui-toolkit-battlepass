using System;
using Project.UI.Navigation;
using Project.UI.Panels;

namespace Project.UI.MainScreen
{
    public sealed class MainButtonActionFactory
    {
        private readonly Func<IPanelService> _panelServiceProvider;
        private readonly PanelViewFactory _panelViewFactory;

        public MainButtonActionFactory(Func<IPanelService> panelServiceProvider, PanelViewFactory panelViewFactory)
        {
            _panelServiceProvider = panelServiceProvider;
            _panelViewFactory = panelViewFactory;
        }

        public IMainButtonAction Create(MainButtonConfig config)
        {
            return config.ActionType switch
            {
                MainButtonActionType.OpenPanel => new OpenPanelButtonAction(
                    _panelServiceProvider(),
                    _panelViewFactory,
                    config.PanelId),
                _ => throw new ArgumentOutOfRangeException(nameof(config), config.ActionType, "Unsupported button action type.")
            };
        }
    }
}
