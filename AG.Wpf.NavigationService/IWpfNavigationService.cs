using GalaSoft.MvvmLight.Views;

namespace AG.Wpf.NavigationService
{
    public interface IWpfNavigationService : INavigationService
    {
        object WindowParameter { get; }

        bool CanGoBack();
        bool CanGoForward();
        void GoForward();
        void OpenWindow(string key, bool isTopMost, bool isDialog);
        void OpenWindow(string key, object parameter, bool isTopMost, bool isDialog);
    }
}
