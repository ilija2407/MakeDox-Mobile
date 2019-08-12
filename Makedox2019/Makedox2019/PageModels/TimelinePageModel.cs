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

            GroupedMovies = db.All<Movie>().ToList().GroupBy(x => x.Location).ToDictionary(x => x.Key, x => x.Select(y => new TimelineItem(y.ID, y.Title, y.StartTime, y.EndTime)).ToList());
            RaisePropertyChanged(nameof(GroupedMovies));
        }

        public TimelinePageModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public ICommand SelectMovieCommand => new DelegateCommand<int?>(async id => await _navigationService.NavigateAsync(nameof(EventDetailsPage), new NavigationParameters { { "Id", id } }));
    }
}
