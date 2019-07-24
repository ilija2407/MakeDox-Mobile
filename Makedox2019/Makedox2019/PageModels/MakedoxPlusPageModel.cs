using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class MakedoxPlusPageModel : FreshMvvm.FreshBasePageModel
    {
        #region Commands
        public ICommand NavigateToUpcomingEventsPageCommand { get; set; }
        public ICommand NavigateToTimeLinePageCommand { get; set; }
        public ICommand NavigateToMakedoxPageCommand { get; set; }
        public ICommand NavigateToMenuPageCommand { get; set; }
        public ICommand NavigateToFilmsPageCommand { get; set; }


        public ICommand NavigateToDocPageCommand { get; set; }
        public ICommand NavigateToWorkshopPageCommand { get; set; }
        public ICommand NavigateToPhotoPageCommand { get; set; }
        public ICommand NavigateToMusicPageCommand { get; set; }

        #endregion

        public MakedoxPlusPageModel()
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

            NavigateToDocPageCommand = new Command(NavigateToDocPage);
            NavigateToWorkshopPageCommand = new Command(NavigateToWorkshopPage);
            NavigateToPhotoPageCommand = new Command(NavigateToPhotoPage);
            NavigateToMusicPageCommand = new Command(NavigateToMusicPage);

        }

        private async void NavigateToMusicPage(object obj)
        {
            //push a basic page Modally
            var page = FreshPageModelResolver.ResolvePageModel<MusicPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
        }

        private async void NavigateToPhotoPage(object obj)
        {
            //push a basic page Modally
            var page = FreshPageModelResolver.ResolvePageModel<PhotoExebitionPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
        }

        private async void NavigateToWorkshopPage(object obj)
        {
            //push a basic page Modally
            var page = FreshPageModelResolver.ResolvePageModel<WorkshopsPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
        }

        private async void NavigateToDocPage(object obj)
        {
            //push a basic page Modally
            var page = FreshPageModelResolver.ResolvePageModel<DocTalksPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page, "secondNavPage");
            await CoreMethods.PushNewNavigationServiceModal(basicNavContainer, new FreshBasePageModel[] { page.GetModel() });
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
