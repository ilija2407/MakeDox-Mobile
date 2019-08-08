using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class WorkshopsPageModel : ViewModelBase
    {
        public WorkshopsPageModel(INavigationService navigationService)
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
