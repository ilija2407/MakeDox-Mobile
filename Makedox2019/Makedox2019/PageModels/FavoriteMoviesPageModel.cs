using Makedox2019.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class FavoriteMoviesPageModel : FreshMvvm.FreshBasePageModel
    {
        #region Commands
        public ICommand NavigateToUpcomingEventsPageCommand { get; set; }
        public ICommand NavigateToTimeLinePageCommand { get; set; }
        public ICommand NavigateToMakedoxPageCommand { get; set; }
        public ICommand NavigateToMenuPageCommand { get; set; }
        public ICommand NavigateToFilmsPageCommand { get; set; }
        #endregion
        public List<Movie> FavoriteMovies { get; set; }

        public FavoriteMoviesPageModel()
        {
            var db = Realm.GetInstance();
            FavoriteMovies = db.All<Movie>().Where(x => !string.IsNullOrEmpty(x.IsFavorite) && x.IsFavorite.ToLowerInvariant() == "1").ToList();
        }



        #region Methods

        private void SetCommands()
        {
            NavigateToFilmsPageCommand = new Command(NavigateToFilmsPage);
            NavigateToUpcomingEventsPageCommand = new Command(NavigateToUpcommingEventsPage);
            NavigateToTimeLinePageCommand = new Command(NavigateToTimeLinePage);
            NavigateToMakedoxPageCommand = new Command(NavigateToMakedoxPage);
            NavigateToMenuPageCommand = new Command(NavigateToMenuPage);

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
