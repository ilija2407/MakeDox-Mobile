using Makedox2019.Pages;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace Makedox2019.PageModels
{
    public class MenuPageModel : ViewModelBase
    {
        public ICommand NavigateToInfoPageCommand => new DelegateCommand(async () => await _navigationService.NavigateAsync(nameof(InfoPage)));
        public ICommand NavigateToGuestPageCommand => new DelegateCommand(async () => await _navigationService.NavigateAsync(nameof(GuestServicePage)));
        public ICommand NavigateToVenuesPageCommand => new DelegateCommand(async () => await _navigationService.NavigateAsync(nameof(VenuesPage)));
        public ICommand NavigateToTicketsPageCommand => new DelegateCommand(async () => await _navigationService.NavigateAsync(nameof(TicketsPage)));
        public ICommand NavigateToSocialPageCommand => new DelegateCommand(async () => await _navigationService.NavigateAsync(nameof(SocialPage)));
        public ICommand NavigateToMapsPageCommand => new DelegateCommand(async () => await _navigationService.NavigateAsync(nameof(MapsPage)));

        public MenuPageModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
