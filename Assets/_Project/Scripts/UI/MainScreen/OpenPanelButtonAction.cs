using Project.UI.Navigation;
using Project.UI.Panels;

namespace Project.UI.MainScreen
{
    public sealed class OpenPanelButtonAction : IMainButtonAction
    {
        private readonly IPanelService _panelService;
        private readonly PanelViewFactory _panelViewFactory;
        private readonly string _panelId;

        public OpenPanelButtonAction(IPanelService panelService, PanelViewFactory panelViewFactory, string panelId)
        {
            _panelService = panelService;
            _panelViewFactory = panelViewFactory;
            _panelId = panelId;
        }

        public void Execute()
        {
            var panel = _panelViewFactory.Create(_panelId);
            _panelService.Open(panel);
        }
    }
}
