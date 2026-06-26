namespace Project.UI.Panels
{
    public sealed class TestPanelViewModel
    {
        public TestPanelViewModel(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; }
        public string Content { get; }
    }
}
