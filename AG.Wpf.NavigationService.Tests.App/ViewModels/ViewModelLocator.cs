using AG.Wpf.NavigationService.Tests.App.Data;
using AG.Wpf.NavigationService.Tests.App.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class ViewModelLocator
    {
        #region ViewModels
        public MainViewModel Main { get { return ServiceLocator.Current.GetInstance<MainViewModel>(); } }

        public Page1ViewModel Page1 { get { return ServiceLocator.Current.GetInstance<Page1ViewModel>(); } }
        public Page2ViewModel Page2 { get { return ServiceLocator.Current.GetInstance<Page2ViewModel>(); } }
        public Page3ViewModel Page3 { get { return ServiceLocator.Current.GetInstance<Page3ViewModel>(); } }

        public Ctrl1ViewModel Control1 { get { return ServiceLocator.Current.GetInstance<Ctrl1ViewModel>(); } }
        public Ctrl2ViewModel Control2 { get { return ServiceLocator.Current.GetInstance<Ctrl2ViewModel>(); } }
        public Ctrl3ViewModel Control3 { get { return ServiceLocator.Current.GetInstance<Ctrl3ViewModel>(); } }
        #endregion

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
                SimpleIoc.Default.Register<IFrameNavigationService, FrameDesignNavigationService>();
                SimpleIoc.Default.Register<IContentNavigationService, ContentDesignNavigationService>();
                SimpleIoc.Default.Register<IWindowNavigationService, WindowDesignNavigationService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
                SimpleIoc.Default.Register<IFrameNavigationService>(() => CreateFrameNavSvc());
                SimpleIoc.Default.Register<IContentNavigationService>(() => CreateContentNavSvc());
                SimpleIoc.Default.Register<IWindowNavigationService>(() => CreateWindowNavSvc());
            }

            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<Page1ViewModel>();
            SimpleIoc.Default.Register<Page2ViewModel>();
            SimpleIoc.Default.Register<Page3ViewModel>();

            SimpleIoc.Default.Register<Ctrl1ViewModel>();
            SimpleIoc.Default.Register<Ctrl2ViewModel>();
            SimpleIoc.Default.Register<Ctrl3ViewModel>();
        }

        private MainWindow GetMainWindow()
        {
            return App.Current.MainWindow as MainWindow;
        }

        private FrameNavigationService CreateFrameNavSvc()
        {
            var fns = new FrameNavigationService(() => GetMainWindow().MainFrame);
            fns.ConfigurePage(typeof(Page1ViewModel).Name, @"Views\Page1View.xaml");
            fns.ConfigurePage(typeof(Page2ViewModel).Name, @"Views\Page2View.xaml");
            fns.ConfigurePage(typeof(Page3ViewModel).Name, @"Views\Page3View.xaml");
            return fns;
        }

        private ContentNavigationService CreateContentNavSvc()
        {
            var cns = new ContentNavigationService(() => GetMainWindow().MainContent);
            cns.ConfigureView(typeof(Ctrl1ViewModel).Name, () => new Ctrl1View());
            cns.ConfigureView(typeof(Ctrl2ViewModel).Name, () => new Ctrl2View());
            cns.ConfigureView(typeof(Ctrl3ViewModel).Name, () => new Ctrl3View());
            return cns;
        }

        private WindowNavigationService CreateWindowNavSvc()
        {
            var wns = new WindowNavigationService();
            wns.ConfigureWindow("window", () => new Dialog1Window());
            return wns;
        }

    }
}