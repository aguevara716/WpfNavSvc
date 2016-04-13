using GalaSoft.MvvmLight.Views;

namespace AG.Wpf.NavigationService
{
    public interface IFrameNavigationService : INavigationService
    {
        new string CurrentPageKey { get; set; }
        object ViewParameter { get; }

        bool CanGoBack();
        bool CanGoForward();
        void GoForward();
    }
}
