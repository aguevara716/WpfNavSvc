namespace AG.Wpf.NavigationService
{
    public interface IFrameNavigationService : IWpfNavigationService
    {
        new string CurrentPageKey { get; set; }
        object ViewParameter { get; }

        //bool CanGoBack();
        //bool CanGoForward();
        //void GoForward();
    }
}
