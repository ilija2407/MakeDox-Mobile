using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class MapsPageModel : ViewModelBase
    {
        public MapsPageModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

    }
}
