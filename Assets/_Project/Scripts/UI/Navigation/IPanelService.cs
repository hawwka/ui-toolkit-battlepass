namespace Project.UI.Navigation
{
    public interface IPanelService
    {
        void Open(IPanelView panel);
        void CloseCurrent();
    }
}
