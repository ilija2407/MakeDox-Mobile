using Makedox2019.Pages;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class VenuesPageModel : ViewModelBase
    {
        public VenuesPageModel(INavigationService navigationService)
            : base(navigationService)
        {
            SetCommands();
        }

        public ICommand GoBack { get; set; }
        public ICommand NavigateToKinoMileniumCommand { get; set; }
        public ICommand NavigateToKurshumliCommand { get; set; }
        public ICommand NavigateToKurshumliOutCommand { get; set; }
        public ICommand NavigateToMkcCommand { get; set; }

        private void SetCommands()
        {
            GoBack = new Command(Back);
            NavigateToKinoMileniumCommand = new Command(NavigateToKinoMilenium);
            NavigateToKurshumliCommand = new Command(NavigateToKurshumli);
            NavigateToKurshumliOutCommand = new Command(NavigateToKurshumliOut);
            NavigateToMkcCommand = new Command(NavigateToMkc);

        }

        private async void NavigateToMkc(object obj)
        {
            await _navigationService.NavigateAsync($"{nameof(CategoryPage)}?Location=MKC");
        }

        private async void NavigateToKurshumliOut(object obj)
        {
            await _navigationService.NavigateAsync($"{nameof(CategoryPage)}?Location=Kurshumli Out");
        }

        private async void NavigateToKurshumli(object obj)
        {
            await _navigationService.NavigateAsync($"{nameof(CategoryPage)}?Location=Kurshumli An");
        }

        private async void NavigateToKinoMilenium(object obj)
        {
             await _navigationService.NavigateAsync($"{nameof(CategoryPage)}?Location=Кино Милениум");
        }

        private void Back(object obj)
        {
            _navigationService.GoBackAsync();
        }

    }
}
