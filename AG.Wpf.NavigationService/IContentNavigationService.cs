namespace AG.Wpf.NavigationService
{
    public interface IContentNavigationService : IWpfNavigationService
    {
        new string CurrentPageKey { get; set; }
        object ViewParameter { get; }

        bool CanGoBack();
        bool CanGoForward();
        void GoForward();
    }
}
