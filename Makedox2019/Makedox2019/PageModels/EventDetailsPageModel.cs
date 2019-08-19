using Makedox2019.Models;
using Makedox2019.Pages;
using Plugin.LocalNotification;
using Prism.Mvvm;
using Prism.Navigation;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class EventDetailsPageModel : ViewModelBase
    {
        public int ID { get; set; }
        public Movie Model { get; set; }

        public bool ItemVisibility { get; set; }

        public bool OneLinerVisibility { get; set; }

        public ICommand WatchTrailerCommand => new Command<Movie>((movie) =>
        {
            if (movie!=null)
            {
                if (!String.IsNullOrEmpty(movie.Trailer))
                {
                    var trailer = movie.Trailer;
                    Device.OpenUri(new Uri((string)trailer));
                }
            }
           
            //await _navigationService.NavigateAsync(nameof(EventDetailsPage), new NavigationParameters { { "Id", movie.ID } });
        });

        public EventDetailsPageModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters!= null)
            {
                if (parameters.Count > 0)
                {
                    ID = (int)parameters["Id"];
                    ItemVisibility = true;
                    OneLinerVisibility = false;

                    var db = Realm.GetInstance();
                    var movie = db.Find<Movie>(ID);
                    if (movie == null)
                        return;

                    if (movie.Type != "Movies")
                    {
                        ItemVisibility = false;
                        OneLinerVisibility = true;
                    }

                    Model = movie;
                }

            }

        }

        public ICommand FavoriteMovieCommand => new Command(() =>
        {
            var db = Realm.GetInstance();
            db.Write(() =>
            {
                var notifications = db.All<Notification>();
                Model.IsFavorite = Model.IsFavorite == false ? true : false;
                var currentNotif = notifications.FirstOrDefault(x => x.MovieId == Model.ID);
                if (currentNotif != null)
                    NotificationCenter.Current.Cancel(currentNotif.NotificationId);

                if (Model.IsFavorite)
                {
                    if (currentNotif != null)
                    {
                        db.Remove(currentNotif);
                        var notif = new Notification(notifications.Count(), new Random(305006489).Next(100000, 600000), Model.ID);

                        db.Add(notif);
                        var time = Model.StartTime.Value.AddMinutes(-30).DateTime;
                        if (time < DateTime.Now)
                            time = DateTime.Now.AddMinutes(1);

                        var notification = new NotificationRequest
                        {
                            NotificationId = notif.NotificationId,
                            Title = Model.Title,
                            Description = $"will be displayed at {Model.StartTime?.ToString("dd/MM/yyyy HH:mm")}",
                            ReturningData = Model.ID.ToString(),// Returning data when tapped on notification.
                            NotifyTime = time // Used for Scheduling local notification, if not specified notification will show immediately.
                        };
                        NotificationCenter.Current.Show(notification);
                    }

                }
            });
        });
    }
}
