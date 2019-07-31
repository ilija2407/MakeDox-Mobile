using Makedox2019.Models;
using Plugin.LocalNotification;
using Realms;
using System;
using System.Linq;
using System.Threading.Tasks;
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

        public ICommand DetailsCommand => new Command<Movie>(async (movie) =>
        {
            await CoreMethods.PushPageModel<EventDetailsPageModel>(movie.ID);
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
        }



        public override void Init(object initData)
        {
            base.Init(initData);
            try
            {

            var db = Realm.GetInstance();
            UpComingEvents = db.All<Movie>().OrderBy(x => x.StartTime).AsRealmCollection();
                db.Write(() =>
                {
                    var evtList = db.All<Movie>().Where(x => x.StartTime > DateTimeOffset.Now).ToList();
                    var q = evtList.Count();
                    evtList.First().StartTime = DateTime.Now.AddMinutes(30).AddSeconds(30);
                    db.RemoveAll<Notification>();
                    NotificationCenter.Current.CancelAll();
                    var i = 1;
                    foreach (var evt in evtList)
                    {
                        var notif = new Notification(i++, new Random(305006489).Next(100000, 600000));

                        db.Add(notif);
                        var time = evt.StartTime.Value.AddMinutes(-30).DateTime;
                        if (time < DateTime.Now)
                            time = DateTime.Now.AddMinutes(1);

                        var notification = new NotificationRequest
                        {
                            NotificationId = notif.Id,
                            Title = evt.Title,
                            Description = $"will be displayed at {evt.StartTime.Value.ToString("dd/MM/yyyy HH:mm")}",
                            ReturningData = evt.ID.ToString(),// Returning data when tapped on notification.
                        NotifyTime = time // Used for Scheduling local notification, if not specified notification will show immediately.
                    };
                        NotificationCenter.Current.Show(notification);
                    }
                });
            }
            catch(Exception ex)
            {

            }
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
