using System;
using System.Collections.Generic;
using Project.UI.Core;
using UnityEngine.UIElements;

namespace Project.UI.MainScreen
{
    public sealed class MainScreenView : IDisposable
    {
        private readonly VisualElement _root;
        private readonly MainScreenViewModel _viewModel;
        private readonly DisposableBinding _binding = new();
        private readonly List<Button> _buttons = new();

        public MainScreenView(VisualElement root, MainScreenViewModel viewModel)
        {
            _root = root;
            _viewModel = viewModel;
        }

        public DisposableBinding Bind()
        {
            _binding.Bind(_viewModel.Buttons, RebuildButtons);
            return _binding;
        }

        private void RebuildButtons(IReadOnlyList<MainButtonViewModel> buttons)
        {
            ClearButtons();

            foreach (var buttonViewModel in buttons)
            {
                var slot = GetSlot(buttonViewModel.Position);
                var button = new Button { text = buttonViewModel.Label };
                button.name = $"main-button-{buttonViewModel.Id}";
                button.AddToClassList("main-button");
                button.clicked += buttonViewModel.Action.Execute;
                slot.Add(button);
                _buttons.Add(button);
            }
        }

        private VisualElement GetSlot(MainButtonPosition position)
        {
            return position switch
            {
                MainButtonPosition.BottomLeft => _root.Q<VisualElement>("left-button-slot"),
                MainButtonPosition.BottomRight => _root.Q<VisualElement>("right-button-slot"),
                _ => throw new ArgumentOutOfRangeException(nameof(position), position, "Unsupported button position.")
            };
        }

        private void ClearButtons()
        {
            _root.Q<VisualElement>("left-button-slot")?.Clear();
            _root.Q<VisualElement>("right-button-slot")?.Clear();
            _buttons.Clear();
        }

        public void Dispose() => _binding.Dispose();
    }
}
