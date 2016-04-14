using System;
using System.Windows.Controls;

namespace AG.Wpf.NavigationService.Tests.Pages
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public static Uri Path { get { return new Uri(@"Pages/Page2.xaml", UriKind.Relative); } }

        public Page2()
        {
            InitializeComponent();
        }
    }
}
