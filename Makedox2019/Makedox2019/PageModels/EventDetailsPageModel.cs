using Makedox2019.Models;
using Makedox2019.Pages;
using Prism.Mvvm;
using Prism.Navigation;
using Realms;
using System;
using System.Collections.Generic;
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
            var trailer = movie.Trailer;
            Device.OpenUri(new Uri((string)trailer));
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
    }
}
