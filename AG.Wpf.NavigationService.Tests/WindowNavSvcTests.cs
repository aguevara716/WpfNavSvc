using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AG.Wpf.NavigationService.WindowNav;

namespace AG.Wpf.NavigationService.Tests
{
    [TestClass]
    public class WindowNavSvcTests
    {
        private WindowNavigationService navSvc;

        [TestInitialize]
        public void BeforeEach()
        {
            navSvc = new WindowNavigationService();
        }

        [TestMethod]
        public void TestConfiguringWindowDuplicateKeys()
        {
            try
            {
                navSvc.ConfigureWindow<Window1>(typeof(Window1).Name);
                navSvc.ConfigureWindow<Window2>(typeof(Window1).Name);
                Assert.Fail("The exception wasn't thrown");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        //[Ignore]
        public void TestConfiguringWindowDuplicateViews()
        {
            try
            {
                navSvc.ConfigureWindow<Window1>(typeof(Window1).Name);
                navSvc.ConfigureWindow<Window1>(typeof(Window1).Name + "-1");
                Assert.Fail("The exception wasn't thrown");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private class Window1 : System.Windows.Window { }
        private class Window2 : System.Windows.Window { }
        private class Window3 : System.Windows.Window { }
    }
}
