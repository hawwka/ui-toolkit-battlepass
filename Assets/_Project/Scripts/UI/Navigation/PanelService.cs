using System;

namespace Project.UI.Navigation
{
    public sealed class PanelService : IPanelService
    {
        private readonly ModalOverlayView _overlay;
        private readonly IBackgroundBlurService _blurService;
        private IPanelView _currentPanel;
        private PanelOpenScaleAnimation _openAnimation;

        public PanelService(ModalOverlayView overlay, IBackgroundBlurService blurService)
        {
            _overlay = overlay;
            _blurService = blurService;
            _overlay.BackdropClicked += CloseCurrent;
        }

        public void Open(IPanelView panel)
        {
            if (panel == null)
                throw new ArgumentNullException(nameof(panel));

            CloseCurrent();

            _currentPanel = panel;
            _currentPanel.CloseRequested += OnPanelCloseRequested;
            _overlay.SetPanel(panel.Root);
            _blurService.Enable();
            _overlay.Show();
            _openAnimation = PanelOpenScaleAnimation.Play(panel.Root);
            panel.OnOpened();
        }

        public void CloseCurrent()
        {
            if (_currentPanel == null)
                return;

            _openAnimation?.Dispose();
            _openAnimation = null;

            _currentPanel.CloseRequested -= OnPanelCloseRequested;
            _currentPanel.OnClosed();
            _currentPanel = null;
            _overlay.Hide();
            _blurService.Disable();
        }

        private void OnPanelCloseRequested() => CloseCurrent();
    }
}
