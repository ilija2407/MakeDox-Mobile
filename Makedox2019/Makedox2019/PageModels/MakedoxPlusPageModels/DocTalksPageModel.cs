using Makedox2019.Models;
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
    public class DocTalksPageModel: ViewModelBase
    {
        public List<Movie> MoviesList { get; set; }
        public DocTalksPageModel(INavigationService navigationService)
            :base(navigationService)
        {
            SetCommands();
        }

        public ICommand GoBack { get; set; }

        private void SetCommands()
        {
            GoBack = new Command(Back);
        }

        private void Back(object obj)
        {
            _navigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //Title = (string)parameters["Category"];
            var db = Realm.GetInstance();
            MoviesList = db.All<Movie>().Where(x => x.Category == "DOC TALKS UNDER THE FIG TREE").OrderBy(x => x.StartTime).ToList();
            RaisePropertyChanged(nameof(MoviesList));
        }
    }
}
