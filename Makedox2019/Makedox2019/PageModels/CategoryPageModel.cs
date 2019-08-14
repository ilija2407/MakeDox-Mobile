﻿using Makedox2019.Models;
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
    public class CategoryPageModel : ViewModelBase
    {
        public List<Movie> MoviesList { get; set; }
        public string Title { get; set; }

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

                    if (movie.IsFavorite)
                    {
                        db.Remove(currentNotif);
                        var notif = new Notification(notifications.Count(), new Random(305006489).Next(100000, 600000), movie.ID);

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

        public ICommand DetailsCommand => new Command<Movie>(async (movie) =>
        {
            await _navigationService.NavigateAsync(nameof(EventDetailsPage), new NavigationParameters { { "Id", movie.ID } });
        });

        public CategoryPageModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public bool HasCoverImage { get; set; }
        public ImageSource CoverImageSource { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (string.IsNullOrEmpty(Title))
            {
                Title = (string)parameters["Category"];
                if (Title != null)
                {
                    SetupCoverImage(Title);
                    var db = Realm.GetInstance();
                    MoviesList = db.All<Movie>().Where(x => x.Category == Title).OrderBy(x => x.StartTime).ToList();
                }

                Title = (string)parameters["Location"];
                if (Title != null)
                {
                    var db = Realm.GetInstance();
                    MoviesList = db.All<Movie>().Where(x => x.Location == Title).OrderBy(x => x.StartTime).ToList();
                }
            }

            if (Title == "Workshops")
            {
                MoviesList.Distinct().ToList();
            }
            RaisePropertyChanged(nameof(MoviesList));
        }

        private void SetupCoverImage(string title)
        {
            string url = string.Empty;
            switch (title.ToLowerInvariant())
            {
                case "main selection":
                    url = "https://user-images.githubusercontent.com/20807086/62822618-81842680-bb75-11e9-9286-818af5b6433a.png";
                    break;
                case "newcomers":
                    url = "https://user-images.githubusercontent.com/20807086/62822616-80eb9000-bb75-11e9-90cb-e841c9971e4f.png";
                    break;
                case "country in focus: germany":
                    url = "https://user-images.githubusercontent.com/20807086/62822617-81842680-bb75-11e9-86f1-1e5c421d025d.png";
                    break;
                case "short dox":
                    url = "https://user-images.githubusercontent.com/20807086/62822619-81842680-bb75-11e9-9d35-028e789df1d2.png";
                    break;
                case "student dox":
                    url = "https://user-images.githubusercontent.com/20807086/62822620-81842680-bb75-11e9-9204-ff03c6222c76.png";
                    break;
            }
            if (string.IsNullOrEmpty(url))
                return;

            CoverImageSource = url;
            HasCoverImage = true;
        }
    }
}
