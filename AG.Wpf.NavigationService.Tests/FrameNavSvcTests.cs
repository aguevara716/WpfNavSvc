using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using AG.Wpf.NavigationService.FrameNav;
using AG.Wpf.NavigationService.Tests.Pages;

namespace AG.Wpf.NavigationService.Tests
{
    [TestClass]
    public class FrameNavSvcTests
    {
        private Frame targetFrame;
        private FrameNavigationService navSvc;

        [TestInitialize]
        public void BeforeEach()
        {
            var mainWin = new MainWindow();
            //mainWin.Show();
            targetFrame = mainWin.MainFrame;
            navSvc = new FrameNavigationService(() => { return targetFrame; });

            navSvc.ConfigurePage(typeof(Page1).Name, Page1.Path);
            navSvc.ConfigurePage(typeof(Page2).Name, Page2.Path);
            navSvc.ConfigurePage(typeof(Page3).Name, Page3.Path);
        }

        #region ConfigurePage Tests
        [TestMethod]
        public void TestConfiguringDuplicateKeys()
        {
            try
            {
                navSvc.ConfigurePage(typeof(Page1).Name, Page1.Path);
                Assert.Fail("The exception wasn't thrown");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void TestConfiguringDuplicatePages()
        {
            try
            {
                navSvc.ConfigurePage(typeof(Page1).Name + "-1", Page1.Path);
                Assert.Fail("The exception wasn't thrown");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region NavigateTo Tests
        [TestMethod]
        public void TestNavigateToGoodParams()
        {
            Action<string, Uri> nav = (pageKey, pageUri) =>
            {
                var param = $"{pageKey} param";
                navSvc.NavigateTo(pageKey, param);

                Assert.AreEqual(pageUri, targetFrame.Source);
                Assert.AreEqual(pageKey, navSvc.CurrentPageKey);
                Assert.AreEqual(param, navSvc.ViewParameter);
            };

            Assert.IsFalse(navSvc.CanGoBack());
            Assert.IsFalse(navSvc.CanGoForward());

            nav(typeof(Page1).Name, Page1.Path);
            Assert.IsFalse(navSvc.CanGoBack());
            Assert.IsFalse(navSvc.CanGoForward());

            nav(typeof(Page2).Name, Page2.Path);
            Assert.IsTrue(navSvc.CanGoBack());
            Assert.IsFalse(navSvc.CanGoForward());

            nav(typeof(Page3).Name, Page3.Path);
            Assert.IsTrue(navSvc.CanGoBack());
            Assert.IsFalse(navSvc.CanGoForward());
        }

        [TestMethod]
        public void TestNavigateToWithInvalidKey()
        {
            try
            {
                navSvc.NavigateTo("This view doesn't exist");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }
        #endregion

        #region Forward/Back Tests
        [TestMethod]
        public void TestGoBack()
        {
            var uc1Model = "uc1 model";
            navSvc.NavigateTo(typeof(Page1).Name, uc1Model);
            navSvc.NavigateTo(typeof(Page2).Name);

            navSvc.GoBack();

            Assert.AreEqual(Page1.Path, targetFrame.Source);
            Assert.AreEqual(typeof(Page1).Name, navSvc.CurrentPageKey);
            Assert.AreEqual(uc1Model, navSvc.ViewParameter);
        }

        [TestMethod]
        public void TestGoForward()
        {
            var uc2Model = "uc2 model";
            navSvc.NavigateTo(typeof(Page1).Name);
            navSvc.NavigateTo(typeof(Page2).Name, uc2Model);

            navSvc.GoBack();
            navSvc.GoForward();

            Assert.AreEqual(Page2.Path, targetFrame.Source);
            Assert.AreEqual(typeof(Page2).Name, navSvc.CurrentPageKey);
            Assert.AreEqual(uc2Model, navSvc.ViewParameter);
        }
        #endregion

    }
}
