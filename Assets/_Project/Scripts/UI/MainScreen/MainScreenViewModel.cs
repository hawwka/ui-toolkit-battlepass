using System.Collections.Generic;
using Project.UI.Core;

namespace Project.UI.MainScreen
{
    public sealed class MainScreenViewModel
    {
        public ObservableProperty<IReadOnlyList<MainButtonViewModel>> Buttons { get; } = new();

        public MainScreenViewModel(IReadOnlyList<MainButtonViewModel> buttons)
        {
            Buttons.Value = buttons;
        }
    }
}
