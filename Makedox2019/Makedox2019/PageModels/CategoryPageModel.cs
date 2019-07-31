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
    public class CategoryPageModel : FreshMvvm.FreshBasePageModel
    {
        #region Properties
        public List<Movie> MoviesList { get; set; }


        #endregion

        #region Commands
        public ICommand NavigateToUpcomingEventsPageCommand { get; set; }
        public ICommand NavigateToTimeLinePageCommand { get; set; }
        public ICommand NavigateToMakedoxPageCommand { get; set; }
        public ICommand NavigateToMenuPageCommand { get; set; }
        public ICommand NavigateToFilmsPageCommand { get; set; }

        public ICommand FavoriteMovieCommand => new Command<Movie>(movie =>
        {

            var db = Realm.GetInstance();
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

        public ICommand DetailsCommand => new Command<Movie>(async (movie) =>
        {
            await CoreMethods.PushPageModel<EventDetailsPageModel>(movie.ID);
        });
        #endregion

        public string Title { get; set; }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            var db = Realm.GetInstance();
            MoviesList = db.All<Movie>().Where(x => x.Category == Title).OrderBy(x => x.StartTime).ToList();
            RaisePropertyChanged(nameof(MoviesList));
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Title = (string)initData;
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
