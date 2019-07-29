using Makedox2019.Models;
using Realms;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class UpcomingEventsPageModel : FreshMvvm.FreshBasePageModel
    {
        #region Commands
        public ICommand NavigateToUpcomingEventsPageCommand { get; set; }
        public ICommand NavigateToTimeLinePageCommand { get; set; }
        public ICommand NavigateToMakedoxPageCommand { get; set; }
        public ICommand NavigateToMenuPageCommand { get; set; }
        public ICommand NavigateToFilmsPageCommand { get; set; }
        public ICommand FavoriteMovieCommand => new Command<Movie>(movie =>
        {

            var db = Realm.GetInstance();
            db.Error -= Db_Error;
            db.Error += Db_Error;
            db.Write(() =>
            {
                try
                {
                    movie.IsFavorite = movie.IsFavorite == false ? true : false;
                }
                catch (Exception ex)
                {

                }
            });
        });

        private void Db_Error(object sender, ErrorEventArgs e)
        {

        }

        public ICommand FavoriteListCommand => new Command(() => CoreMethods.PushPageModel<FavoriteMoviesPageModel>(true));

        #endregion
        public IRealmCollection<Movie> UpComingEvents { get; set; }

        #region CTOR
        public UpcomingEventsPageModel()
        {
            SetCommands();
            var db = Realm.GetInstance();
            UpComingEvents = db.All<Movie>().OrderBy(x => x.StartTime).AsRealmCollection();
        }



        public override void Init(object initData)
        {
            base.Init(initData);
        }

        #endregion


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
