using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class Ctrl3ViewModel : View3ViewModelBase
    {
        #region Variables
        private readonly IContentNavigationService navService;
        #endregion

        #region Binding variables
        #endregion

        #region Commands
        #endregion

        #region Constructors
        public Ctrl3ViewModel(IContentNavigationService cns)
            : base()
        {
            navService = cns;
            //BackCommand = new RelayCommand(BackExecuted, BackCanExecute);
            //ForwardCommand = new RelayCommand(ForwardExecuted, ForwardCanExecute);
        }
        #endregion

        #region Private methods
        #endregion

        #region Commands CanExecute
        protected override bool BackCanExecute()
        {
            return navService.CanGoBack();
        }

        protected override bool ForwardCanExecute()
        {
            return navService.CanGoForward();
        }
        #endregion

        #region Commands Executed
        protected override void BackExecuted()
        {
            navService.GoBack();
        }

        protected override void ForwardExecuted()
        {
            navService.GoForward();
        }
        #endregion
    }
}
