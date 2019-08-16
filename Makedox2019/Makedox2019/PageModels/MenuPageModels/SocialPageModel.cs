using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class SocialPageModel : ViewModelBase
    {
        public ICommand GoBack { get; set; }
        public ICommand OpenLink => new Command(async (link) =>
        {
            try
            {
                //string youtubeUrl = (Device.RuntimePlatform == Device.Android) ? link : 
                Device.OpenUri(new Uri((string)link));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        });
    
    public SocialPageModel(INavigationService navigationService)
            : base(navigationService)
        {
            SetCommands();
        }

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
