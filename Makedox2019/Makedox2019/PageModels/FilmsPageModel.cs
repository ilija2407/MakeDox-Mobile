using Makedox2019.Models;
using Makedox2019.Pages;
using Prism.Commands;
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
    public class FilmsPageModel : ViewModelBase
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

        private List<Category> categoriesList;
        public List<Category> CategoriesList
        {
            get => categoriesList;
            set
            {
                categoriesList = value;
                RaisePropertyChanged(nameof(CategoriesList));
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
        public ICommand SearchCommand { get; private set; }
        public ICommand MovieSelectedCommand { get; private set; }
        public ICommand CategorySelectedCommand => new DelegateCommand<object>(async obj =>
        {
            var category = obj as Category;
            var cat = MoviesList.First(x => x.Category.ToUpperInvariant() == category.Title).Category;
            var q = await _navigationService.NavigateAsync($"{nameof(CategoryPage)}?Category={cat}");
        });

        #endregion

        public FilmsPageModel(INavigationService navigationService)
            : base(navigationService)
        {
            SetCommands();
        }

        #region Methods

        private void SetCommands()
        {
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
                _navigationService.NavigateAsync(nameof(EventDetailsPage), new NavigationParameters { { "Id", item.ID } });
            }

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var db = Realm.GetInstance();
            MoviesList = db.All<Movie>().Where(x => x.Type == "Movies").OrderBy(x => x.StartTime).ToList();
            CategoriesList = MoviesList.GroupBy(x => x.Category).Select(x => new Category { Title = x.Key.ToUpperInvariant() }).ToList();
        }

        public class Category
        {
            public string Title { get; set; }
        }


        #endregion
    }
}
