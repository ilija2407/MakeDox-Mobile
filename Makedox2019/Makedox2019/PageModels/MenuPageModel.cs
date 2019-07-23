using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class MenuPageModel : FreshMvvm.FreshBasePageModel
    {
        #region Commands
        public ICommand NavigateToUpcomingEventsPageCommand { get; set; }
        public ICommand NavigateToTimeLinePageCommand { get; set; }
        public ICommand NavigateToMakedoxPageCommand { get; set; }
        public ICommand NavigateToMenuPageCommand { get; set; }
        public ICommand NavigateToFilmsPageCommand { get; set; }

        public ICommand NavigateToInfoPageCommand { get; set; }
        public ICommand NavigateToGuestPageCommand { get; set; }
        public ICommand NavigateToVenuesPageCommand { get; set; }
        public ICommand NavigateToTicketsPageCommand { get; set; }
        public ICommand NavigateToSocialPageCommand { get; set; }
        public ICommand NavigateToMapsPageCommand { get; set; }
        
        #endregion

        public MenuPageModel()
        {
            SetCommands();

        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }


        #region Methods

        private void SetCommands()
        {
            NavigateToFilmsPageCommand = new Command(NavigateToFilmsPage);
            NavigateToUpcomingEventsPageCommand = new Command(NavigateToUpcommingEventsPage);
            NavigateToTimeLinePageCommand = new Command(NavigateToTimeLinePage);
            NavigateToMakedoxPageCommand = new Command(NavigateToMakedoxPage);
            NavigateToMenuPageCommand = new Command(NavigateToMenuPage);

            NavigateToInfoPageCommand = new Command(NavigateToInfoPage);
            NavigateToGuestPageCommand = new Command(NavigateToGuestPage);
            NavigateToVenuesPageCommand = new Command(NavigateToVenuesPage);
            NavigateToTicketsPageCommand = new Command(NavigateToTicketsPage);
            NavigateToSocialPageCommand = new Command(NavigateToSocialPage);
            NavigateToMapsPageCommand = new Command(NavigateToMapsPageAsync);


        }

        private async void NavigateToMapsPageAsync(object obj)
        {
            //push a basic page Modally
            var page = FreshPageModelResolver.ResolvePageModel<MapsPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });

           // CoreMethods.PushPageModel<MapsPageModel>(true);
        }

        private async void NavigateToSocialPage(object obj)
        {
            var page = FreshPageModelResolver.ResolvePageModel<SocialPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
            //CoreMethods.PushPageModel<SocialPageModel>(true);
        }

        private async void NavigateToTicketsPage(object obj)
        {
            var page = FreshPageModelResolver.ResolvePageModel<TicketsPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
            //CoreMethods.PushPageModel<TicketsPageModel>(true);
        }

        private async void NavigateToVenuesPage(object obj)
        {
            var page = FreshPageModelResolver.ResolvePageModel<VenuesPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
            //CoreMethods.PushPageModel<VenuesPageModel>(true);
        }

        private async void NavigateToInfoPage(object obj)
        {
            var page = FreshPageModelResolver.ResolvePageModel<InfoPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
            //CoreMethods.PushPageModel<InfoPageModel>(true);
        }

        private async void NavigateToGuestPage(object obj)
        {
            var page = FreshPageModelResolver.ResolvePageModel<GuestServicePageModel>();
            var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
            //CoreMethods.PushPageModel<GuestServicePageModel>(true);
        }

        private void NavigateToMenuPage(object obj)
        {
            CoreMethods.PushPageModel<MenuPageModel>(true);
        }

        private void NavigateToMakedoxPage(object obj)
        {
            CoreMethods.PushPageModel<MakedoxPlusPageModel>(true);
        }

        private void NavigateToTimeLinePage(object obj)
        {
            CoreMethods.PushPageModel<TimelinePageModel>(true);
        }

        private void NavigateToUpcommingEventsPage(object obj)
        {
            CoreMethods.PushPageModel<UpcomingEventsPageModel>(true);
        }

        private void NavigateToFilmsPage(object obj)
        {
            CoreMethods.PushPageModel<FilmsPageModel>(true);
        }


        #endregion
    }
}
