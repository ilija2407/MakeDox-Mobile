using Makedox2019.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class FilmsPageModel : FreshMvvm.FreshBasePageModel
    {
        #region Properties
        public List<Movie> MoviesList { get; set; }

        private List<Movie> filteredList;
        public List<Movie> FilteredList
        {
            get { return filteredList; }
            set
            {
                filteredList = value;
                RaisePropertyChanged(nameof(FilteredList));
            }
        }

        private bool showMoviesList;
        public bool ShowMoviesList
        {
            get
            {
                return showMoviesList;
            }
            set
            {
                showMoviesList = value;
                RaisePropertyChanged(nameof(ShowMoviesList));
            }
        }

        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                if (value != null)
                {
                    if (MoviesList != null)
                    {
                        searchText = value;
                        if (value != "")
                        {
                            ShowMoviesList = true;

                        }
                        else
                        {
                            ShowMoviesList = false;

                        }

                        var tempRecords = MoviesList.Where(i => i.Title.ToLower().Contains(searchText.ToLower()) || i.Title.ToLower().Contains(searchText.ToLower())).OrderBy(i => i.StartTime);

                        FilteredList = new List<Movie>(tempRecords);
                    }
                    {
                        searchText = value;
                    }

                }
                else
                {
                    ShowMoviesList = false;
                    searchText = null;

                }

                RaisePropertyChanged(nameof(SearchText));

            }
        }
        #endregion

        #region Commands
        public ICommand NavigateToUpcomingEventsPageCommand { get; set; }
        public ICommand NavigateToTimeLinePageCommand { get; set; }
        public ICommand NavigateToMakedoxPageCommand { get; set; }
        public ICommand NavigateToMenuPageCommand { get; set; }
        public ICommand NavigateToFilmsPageCommand { get; set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand MovieSelectedCommand { get; private set; }

        #endregion

        public FilmsPageModel()
        {
            SetCommands();
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            var db = Realm.GetInstance();
            MoviesList = db.All<Movie>().OrderBy(x => x.StartTime).ToList();
        }


        #region Methods

        private void SetCommands()
        {
            NavigateToFilmsPageCommand = new Command(NavigateToFilmsPage);
            NavigateToUpcomingEventsPageCommand = new Command(NavigateToUpcommingEventsPage);
            NavigateToTimeLinePageCommand = new Command(NavigateToTimeLinePage);
            NavigateToMakedoxPageCommand = new Command(NavigateToMakedoxPage);
            NavigateToMenuPageCommand = new Command(NavigateToMenuPage);
            SearchCommand = new Command<object>(Search);
            MovieSelectedCommand = new Command<object>(OnMovieSelected);

        }

        private void Search(object obj)
        {
            //CoreMethods.PushPageModel<SearchResultsPageModel>(FilteredList);
        }


        private void OnMovieSelected(object obj)
        {
            var item = obj as Movie;
            if (item != null)
            {
                ShowMoviesList = false;
                //CoreMethods.PushPageModel<ProductPageModel>(item);
            }

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


        #endregion
    }
}
