namespace Project.UI.MainScreen
{
    public sealed class MainButtonViewModel
    {
        public MainButtonViewModel(MainButtonConfig config, IMainButtonAction action)
        {
            Id = config.Id;
            Label = config.Label;
            IconId = config.IconId;
            Position = config.Position;
            Action = action;
        }

        public string Id { get; }
        public string Label { get; }
        public string IconId { get; }
        public MainButtonPosition Position { get; }
        public IMainButtonAction Action { get; }
    }
}
