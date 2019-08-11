using Makedox2019.Models;
using Makedox2019.Pages;
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

        public ICommand FavoriteMovieCommand => new Command<Movie>(movie => Realm.GetInstance().Write(() => movie.IsFavorite = movie.IsFavorite == false ? true : false));

        public ICommand DetailsCommand => new Command<Movie>(async (movie) =>
        {
            await _navigationService.NavigateAsync(nameof(EventDetailsPage), new NavigationParameters { { "Id", movie.ID } });
        });

        public CategoryPageModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (string.IsNullOrEmpty(Title))
                Title = (string)parameters["Category"];
            var db = Realm.GetInstance();
            MoviesList = db.All<Movie>().Where(x => x.Category == Title).OrderBy(x => x.StartTime).ToList();
            if (Title == "Workshops")
            {
                MoviesList.Distinct().ToList();
            }
            RaisePropertyChanged(nameof(MoviesList));
        }
    }
}
