namespace AG.Wpf.NavigationService.UserControlNav
{
    /// <summary>
    /// This is only meant to be used as a design-time navigation service.
    /// </summary>
    public class ContentDesignNavigationService : IContentNavigationService
    {
        public string CurrentPageKey { get; set; }
        public object ViewParameter { get { return null; } }

        public bool CanGoBack() { return false; }
        public bool CanGoForward() { return false; }
        public void GoBack() { }

        public void GoForward() { }
        public void NavigateTo(string pageKey) { }
        public void NavigateTo(string pageKey, object parameter) { }
    }
}
