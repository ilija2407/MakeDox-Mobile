using Makedox2019.Models;
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

        public EventDetailsPageModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            ID = (int)parameters["Id"];

            var db = Realm.GetInstance();
            var movie = db.Find<Movie>(ID);
            if (movie == null)
                return;

            Model = movie;
        }
    }
}
