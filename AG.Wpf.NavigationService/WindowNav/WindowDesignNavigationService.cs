namespace AG.Wpf.NavigationService.WindowNav
{
    public class WindowDesignNavigationService : IWindowNavigationService
    {
        public object WindowParameter { get { return null; } }

        public void OpenWindow(string key, bool isTopMost, bool isDialog) { }

        public void OpenWindow(string key, object parameter, bool isTopMost, bool isDialog) { }
    }
}
