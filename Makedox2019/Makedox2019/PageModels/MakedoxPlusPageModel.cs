using Makedox2019.Pages;
using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class MakedoxPlusPageModel : ViewModelBase
    {
        #region Commands
        public ICommand NavigateToDocPageCommand { get; set; }
        public ICommand NavigateToWorkshopPageCommand { get; set; }
        public ICommand NavigateToPhotoPageCommand { get; set; }
        public ICommand NavigateToMusicPageCommand { get; set; }
        public ICommand NavigateToCoProPageCommand { get; set; }

        

        #endregion

        public MakedoxPlusPageModel(INavigationService navigationService)
            : base(navigationService)
        {
            SetCommands();
        }

        #region Methods

        private void SetCommands()
        {
            NavigateToDocPageCommand = new Command(NavigateToDocPage);
            NavigateToWorkshopPageCommand = new Command(NavigateToWorkshopPage);
            NavigateToPhotoPageCommand = new Command(NavigateToPhotoPage);
            NavigateToMusicPageCommand = new Command(NavigateToMusicPage);
            NavigateToCoProPageCommand = new Command(NavigateToCoProPage);
        }

        private void NavigateToCoProPage(object obj)
        {
            _navigationService.NavigateAsync(nameof(CoProPage));
            ////push a basic page Modally
            //var page = FreshPageModelResolver.ResolvePageModel<CoProPageModel>();
            //var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            //await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
        }

        private void NavigateToMusicPage(object obj)
        {
            _navigationService.NavigateAsync(nameof(MusicPage));

            //push a basic page Modally
            //var page = FreshPageModelResolver.ResolvePageModel<MusicPageModel>();
            //var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            //await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
        }

        private void NavigateToPhotoPage(object obj)
        {
            _navigationService.NavigateAsync(nameof(PhotoExebitionPage));

            //push a basic page Modally
            //var page = FreshPageModelResolver.ResolvePageModel<PhotoExebitionPageModel>();
            //var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            //await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
        }

        private void NavigateToWorkshopPage(object obj)
        {
            _navigationService.NavigateAsync(nameof(WorkshopsPage));

            //push a basic page Modally
            //var page = FreshPageModelResolver.ResolvePageModel<WorkshopsPageModel>();
            //var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            //await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
        }

        private void NavigateToDocPage(object obj)
        {
            _navigationService.NavigateAsync(nameof(DocTalksPage));

            //push a basic page Modally
            //var page = FreshPageModelResolver.ResolvePageModel<DocTalksPageModel>();
            //var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            //await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
        }


        #endregion
    }
}
