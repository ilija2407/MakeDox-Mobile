using Makedox2019.Controls;
using Makedox2019.Models;
using Makedox2019.Pages;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class TimelinePageModel : ViewModelBase
    {
        public Dictionary<string, List<TimelineItem>> GroupedMovies { get; private set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var db = Realm.GetInstance();

            var movies = db.All<Movie>().ToList();
            foreach (var item in movies)
            {
                db.Write(() =>
                {
                    if (item.Location == "MKC")
                    {
                        item.displayOrder = 3;
                    }

                    if (item.Location == "Kurshumli An")
                    {
                        item.displayOrder = 1;

                    }

                    if (item.Location == "Кино Милениум")
                    {
                        item.displayOrder = 4;

                    }

                    if (item.Location == "Kurshumli Out")
                    {
                        item.displayOrder = 2;

                    }

                    if (item.Location == "Daut Pasha Hammam")
                    {
                        item.displayOrder = 5;

                    }

                    if (item.Location == "Chifte Hammam")
                    {
                        item.displayOrder = 6;

                    }

                    db.Add(item, true);
                });
            }
            GroupedMovies = db.All<Movie>().ToList().OrderBy(i => i.displayOrder).GroupBy(x => x.Location).ToDictionary(x => x.Key, x => x.Select(y => new TimelineItem(y.ID, y.Title, y.StartTime, y.EndTime)).ToList());
            RaisePropertyChanged(nameof(GroupedMovies));
        }

        public TimelinePageModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public ICommand SelectMovieCommand => new DelegateCommand<int?>(async id => await _navigationService.NavigateAsync(nameof(EventDetailsPage), new NavigationParameters { { "Id", id } }));
    }
}
