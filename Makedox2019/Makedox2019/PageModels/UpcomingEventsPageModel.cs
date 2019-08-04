using Makedox2019.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Plugin.LocalNotification;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
        public IRealmCollection<Movie> UpComingEvents { get; set; }
        public IRealmCollection<Movie> FavoriteMovies { get; set; }

        #region CTOR
        public UpcomingEventsPageModel()
        {
            SetCommands();
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }



        public override void Init(object initData)
        {
            base.Init(initData);
            try
            {
                SyncData();
            }
            catch(Exception e)
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

        public async void SyncData()
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync("https://gist.githubusercontent.com/ice-j/2b60034d079a306182160cc1f9c1516f/raw/5ee90a511531c1d0a8a86a113cce612dac2cfa71/movies.json#" + DateTime.Now.Ticks);
                if (!res.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Cannot retrieve movies data at this time. Please make sure you're connected to the internet and try again", "OK");
                    return;
                }

                var stringContent = await res.Content.ReadAsStringAsync();
                var dtConverter = new IsoDateTimeConverter() { DateTimeFormat = "dd/MM/yyyy HH:mm" };
                var booleanConverter = new BooleanConverter();
                var moviesList = JsonConvert.DeserializeObject<List<Movie>>(stringContent, converters: dtConverter);
                if (moviesList?.Count > 0)
                {
                    var db = Realm.GetInstance();

                    var currentMovies = db.All<Movie>().ToList();
                    db.Write(() =>
                    {
                        var i = 1;
                        db.RemoveAll<Notification>();
                        foreach (var movie in moviesList)
                        {
                            bool shouldUpdate = false;
                            if (shouldUpdate = currentMovies.Any(x => x.ID == movie.ID))
                                movie.IsFavorite = currentMovies.FirstOrDefault(x => x.ID == movie.ID).IsFavorite;
                            if (movie.CoverImage?.Contains("data:image/jpeg;base64,") == true)
                                movie.CoverImage = movie.CoverImage.Replace("data:image/jpeg;base64,", "");
                            if (movie.LogoImage?.Contains("data:image/jpeg;base64,") == true)
                                movie.LogoImage = movie.CoverImage.Replace("data:image/jpeg;base64,", "");

                            db.Add(movie, shouldUpdate);

                            var notif = new Notification(i++, new Random(305006489).Next(100000, 600000));

                            db.Add(notif);
                            var time = movie.StartTime.Value.AddMinutes(-30).DateTime;
                            if (time < DateTime.Now)
                                time = DateTime.Now.AddMinutes(1);
                            NotificationCenter.Current.CancelAll();
                            var notification = new NotificationRequest
                            {
                                NotificationId = notif.Id,
                                Title = movie.Title,
                                Description = $"will be displayed at {movie.StartTime.Value.ToString("dd/MM/yyyy HH:mm")}",
                                ReturningData = movie.ID.ToString(),// Returning data when tapped on notification.
                                NotifyTime = time // Used for Scheduling local notification, if not specified notification will show immediately.
                            };
                            NotificationCenter.Current.Show(notification);
                        }
                        UpComingEvents = db.All<Movie>().OrderBy(x => x.StartTime).AsRealmCollection();
                        FavoriteMovies = db.All<Movie>().Where(x => x.IsFavorite).AsRealmCollection();

                        RaisePropertyChanged(nameof(UpComingEvents));
                        RaisePropertyChanged(nameof(FavoriteMovies));
                    });
                }
            }
        }

        #endregion
    }

}
