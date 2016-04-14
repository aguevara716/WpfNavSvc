using System;
using System.Windows.Controls;

namespace AG.Wpf.NavigationService.Tests.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public static Uri Path { get { return new Uri(@"Pages/Page1.xaml", UriKind.Relative); } }

        public Page1()
        {
            InitializeComponent();
        }
    }
}
