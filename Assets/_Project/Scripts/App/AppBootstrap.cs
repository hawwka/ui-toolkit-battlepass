using System.Collections.Generic;
using System.Linq;
using Project.UI.Core;
using Project.UI.MainScreen;
using Project.UI.Navigation;
using Project.UI.Panels;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.App
{
    public sealed class AppBootstrap : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private PanelSettings panelSettings;
        [SerializeField] private VisualTreeAsset mainScreenUxml;
        [SerializeField] private MainButtonConfig[] buttonConfigs;
        [SerializeField] private PanelConfig[] panelConfigs;

        private MainScreenView _mainScreenView;
        private DisposableBinding _binding;
        private PanelService _panelService;

        private void Awake()
        {
            if (uiDocument == null)
                uiDocument = GetComponent<UIDocument>();

            uiDocument.panelSettings = panelSettings;
            uiDocument.visualTreeAsset = mainScreenUxml;

            var root = uiDocument.rootVisualElement;
            var overlayRoot = root.Q<VisualElement>("modal-overlay-root");

            var modalOverlay = new ModalOverlayView(overlayRoot);
            var blurService = new UiToolkitDimBlurService(modalOverlay.BlurLayer, modalOverlay.Backdrop);
            _panelService = new PanelService(modalOverlay, blurService);

            var panelViewFactory = new PanelViewFactory(panelConfigs);
            var actionFactory = new MainButtonActionFactory(() => _panelService, panelViewFactory);

            var buttonViewModels = (buttonConfigs ?? System.Array.Empty<MainButtonConfig>())
                .Where(config => config != null)
                .Select(config => new MainButtonViewModel(config, actionFactory.Create(config)))
                .ToList();

            var mainScreenViewModel = new MainScreenViewModel(buttonViewModels);
            _mainScreenView = new MainScreenView(root, mainScreenViewModel);
            _binding = _mainScreenView.Bind();
        }

        private void OnDestroy()
        {
            _binding?.Dispose();
            _mainScreenView?.Dispose();
            _panelService?.CloseCurrent();
        }
    }
}
