namespace AG.Wpf.NavigationService
{
    /// <summary>
    /// An interface inspired by the MVVM Light Toolkit.
    /// </summary>
    public interface INavigationService
    {
        string CurrentPageKey { get; }
        object ViewParameter { get; }

        bool CanGoBack();
        bool CanGoForward();
        void GoBack();
        void GoForward();
        void NavigateTo(string pageKey);
        void NavigateTo(string pageKey, object parameter);
    }
}
