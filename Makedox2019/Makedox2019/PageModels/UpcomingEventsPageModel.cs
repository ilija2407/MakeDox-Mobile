using Acr.UserDialogs;
using Makedox2019.Models;
using Makedox2019.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Plugin.LocalNotification;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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
    public class UpcomingEventsPageModel : ViewModelBase
    {
        public ICommand FavoriteMovieCommand => new Command<Movie>(movie =>
        {
            var db = Realm.GetInstance();
            db.Write(() =>
            {
                var notifications = db.All<Notification>();
                movie.IsFavorite = movie.IsFavorite == false ? true : false;
                var currentNotif = notifications.FirstOrDefault(x => x.MovieId == movie.ID);
                if (currentNotif != null)
                    NotificationCenter.Current.Cancel(currentNotif.NotificationId);

                if (movie.IsFavorite && movie.StartTime > App.DateTimeNow.AddMinutes(-30))
                {
                    if (currentNotif != null)
                        db.Remove(currentNotif);
                    var rnd = new Random(305006489);
                    var notif = new Notification(rnd.Next(100000, 600000), movie.ID);
                    while (db.All<Notification>().Any(x => x.NotificationId == notif.NotificationId))
                        notif.NotificationId = rnd.Next(100000, 600000);
                    db.Add(notif);
                    var time = movie.StartTime.Value.AddMinutes(-30).DateTime;
                    if (time < DateTime.Now)
                        time = DateTime.Now.AddMinutes(1);

                    var notification = new NotificationRequest
                    {
                        NotificationId = notif.NotificationId,
                        Title = movie.Title,
                        Description = $"will be displayed at {movie.StartTime?.ToString("dd/MM/yyyy HH:mm")}",
                        ReturningData = movie.ID.ToString(),// Returning data when tapped on notification.
                        NotifyTime = time // Used for Scheduling local notification, if not specified notification will show immediately.
                    };
                    NotificationCenter.Current.Show(notification);
                }
            });
        });

        private int _h;
        public int heightreg
        {
            get
            {
                return _h;
            }
            set
            {
                _h = value;

            }
        }

        public ICommand DetailsCommand => new DelegateCommand<Movie>(async (movie) =>
        {
            var id = movie.ID;
            if (movie.Type != "Event" && movie.Type != "Talks")
            {
                var q = await _navigationService.NavigateAsync(nameof(EventDetailsPage), new NavigationParameters { { "Id", id } });
            }

        });


        public IRealmCollection<Movie> UpComingEvents { get; set; }
        public IRealmCollection<Movie> FavoriteMovies { get; set; }
        public List<MovieLists> MoviesModel { get; set; } = new List<MovieLists>();


        public class MovieLists
        {
            public IRealmCollection<Movie> Movies { get; set; }
            public bool ShowAll { get; set; }
        }

        public UpcomingEventsPageModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        async Task SyncData()
        {
            using (var client = new HttpClient())
            {
                var xv = UserDialogs.Instance;
                using (xv.Loading("Loading...", null, null, true, MaskType.Black))
                {
                    var res = await client.GetAsync("https://gist.githubusercontent.com/ice-j/2b60034d079a306182160cc1f9c1516f/raw/movies.json");

                    if (!res.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Cannot retrieve movies data at this time. Please make sure you're connected to the internet and try again", "OK");
                        return;
                    }

                    var stringContent = await res.Content.ReadAsStringAsync();
                    var dtConverter = new IsoDateTimeConverter() { DateTimeFormat = "dd/MM/yyyy HH:mm", Culture = System.Globalization.CultureInfo.GetCultureInfo("MK"), DateTimeStyles = System.Globalization.DateTimeStyles.AssumeUniversal };
                    var booleanConverter = new BooleanConverter();
                    var moviesList = JsonConvert.DeserializeObject<List<Movie>>(stringContent, converters: dtConverter);
                    var rnd = new Random(305006489);
                    if (moviesList?.Count > 0)
                    {
                        var db = Realm.GetInstance();

                        var currentMovies = db.All<Movie>().ToList();
                        db.Write(() =>
                        {
                            db.RemoveAll<Notification>();
                            NotificationCenter.Current.CancelAll();
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



                                if (movie.IsFavorite && movie.StartTime > App.DateTimeNow.AddMinutes(-30))
                                {
                                    var notif = new Notification(rnd.Next(100000, 600000), movie.ID);
                                    while (db.All<Notification>().Any(x => x.NotificationId == notif.NotificationId))
                                        notif.NotificationId = rnd.Next(100000, 600000);

                                    db.Add(notif);
                                    var time = movie.StartTime.Value.AddMinutes(-30).DateTime;
                                    if (time < DateTime.Now)
                                        time = DateTime.Now.AddMinutes(1);

                                    var notification = new NotificationRequest
                                    {
                                        NotificationId = notif.NotificationId,
                                        Title = movie.Title,
                                        Description = $"will be displayed at {movie.StartTime?.ToString("dd/MM/yyyy HH:mm")}",
                                        ReturningData = movie.ID.ToString(),// Returning data when tapped on notification.
                                        NotifyTime = time // Used for Scheduling local notification, if not specified notification will show immediately.
                                    };
                                    NotificationCenter.Current.Show(notification);
                                }
                            }
                            if (currentMovies.Count > 0)
                            {
                                foreach (var item in currentMovies)
                                {
                                    var ss = moviesList.Find(x => x.ID == item.ID);
                                    if (ss != null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        db.Remove(item);
                                    }
                                }
                            }
                            UpComingEvents = db.All<Movie>().Where(x => x.StartTime > App.DateTimeNow).OrderBy(x => x.StartTime).AsRealmCollection();
                            FavoriteMovies = db.All<Movie>().Where(x => x.IsFavorite).AsRealmCollection();
                            MoviesModel = new List<MovieLists>
                            {
                                new MovieLists { Movies = UpComingEvents, ShowAll = true },
                                new MovieLists { Movies = FavoriteMovies }
                            };


                            RaisePropertyChanged(nameof(UpComingEvents));
                            RaisePropertyChanged(nameof(FavoriteMovies));
                            RaisePropertyChanged(nameof(MoviesModel));
                        });
                        res.Dispose();
                    }
                }

            }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (Device.RuntimePlatform == Device.iOS)
                Device.BeginInvokeOnMainThread(async () => await SyncData());
            else
                await SyncData();


        }
    }
}
