using GalaSoft.MvvmLight.Views;

namespace AG.Wpf.NavigationService.UserControlNav
{
    public interface IContentNavigationService : INavigationService
    {
        new string CurrentPageKey { get; set; }
        object ViewParameter { get; }

        bool CanGoBack();
        bool CanGoForward();
        void GoForward();
    }
}
