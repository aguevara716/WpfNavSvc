using AG.Wpf.NavigationService.FrameNav;
using AG.Wpf.NavigationService.Tests.App.Data;
using AG.Wpf.NavigationService.Tests.App.Views;
using AG.Wpf.NavigationService.WindowNav;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class PageViewModelLocator
    {
        private readonly SimpleIoc container = new SimpleIoc();

        #region ViewModels
        public MainViewModel Main { get { return container.GetInstance<MainViewModel>(); } }
        public View1ViewModel Page1 { get { return container.GetInstance<View1ViewModel>(); } }
        public View2ViewModel Page2 { get { return container.GetInstance<View2ViewModel>(); } }
        public View3ViewModel Page3 { get { return container.GetInstance<View3ViewModel>(); } }
        #endregion

        public PageViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                container.Register<IDataService, DesignDataService>();
                container.Register<INavigationService, DesignTimeNavigationService>();
                container.Register<IWindowNavigationService, WindowDesignNavigationService>();
            }
            else
            {
                container.Register<IDataService, DataService>();
                container.Register<INavigationService>(() => CreateContentNavSvc());
                container.Register<IWindowNavigationService>(() => CreateWindowNavSvc());
            }

            container.Register<MainViewModel>();
            container.Register<View1ViewModel>();
            container.Register<View2ViewModel>();
            container.Register<View3ViewModel>();
        }

        private MainWindow GetMainWindow()
        {
            return App.Current.MainWindow as MainWindow;
        }

        private FrameNavigationService CreateContentNavSvc()
        {
            var fns = new FrameNavigationService(() => GetMainWindow().MainFrame);
            fns.ConfigurePage(typeof(View1ViewModel).Name, @"Views/Page1View.xaml");
            fns.ConfigurePage(typeof(View2ViewModel).Name, @"Views/Page2View.xaml");
            fns.ConfigurePage(typeof(View3ViewModel).Name, @"Views/Page3View.xaml");
            return fns;
        }

        private WindowNavigationService CreateWindowNavSvc()
        {
            var wns = new WindowNavigationService(() => App.Current.MainWindow);
            wns.ConfigureWindow<Dialog1Window>("window");
            return wns;
        }

    }
}
