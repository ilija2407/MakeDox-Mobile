using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class TicketsPageModel : ViewModelBase
    {
        public TicketsPageModel(INavigationService navigationService)
            : base(navigationService)
        {
            SetCommands();
        }

        public ICommand GoBack { get; set; }

        private void SetCommands()
        {
            GoBack = new Command(Back);
        }

        private void Back(object obj)
        {
            _navigationService.GoBackAsync();
        }

    }
}
