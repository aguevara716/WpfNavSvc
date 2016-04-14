using System;
using System.Windows.Controls;

namespace AG.Wpf.NavigationService.Tests.Pages
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public static Uri Path { get { return new Uri(@"Pages/Page3.xaml", UriKind.Relative); } }

        public Page3()
        {
            InitializeComponent();
        }
    }
}
