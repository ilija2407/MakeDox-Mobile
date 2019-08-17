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
            CategoriesList = MoviesList.GroupBy(x => x.Category).Select(x => new Category
            {
                Title = x.Key.ToUpperInvariant()                
            }).OrderBy(x => x.CategoryOrder).ToList();
        }

        public class Category
        {
            public string Title { get; set; }


            public int CategoryOrder
            {
                get
                {
                    switch (Title.ToLowerInvariant())
                    {
                        case "main selection":
                            return 1;
                        case "newcomers":
                            return 2;
                        case "short dox":
                            return 3;
                        case "student dox":
                            return 4;
                        case "country in focus: germany":
                            return 5;
                    }
                    return 6;
                }
            }

            public Color CategoryColor
            {
                get
                {
                    switch (Title.ToLowerInvariant())
                    {
                        case "main selection":
                            return Color.FromHex("#780011");
                        case "newcomers":
                            return Color.FromHex("#ff8900");
                        case "short dox":
                            return Color.FromHex("#c00044");
                        case "student dox":
                            return Color.FromHex("#007637");
                        case "country in focus: germany":
                            return Color.FromHex("#008db4");
                        case "out of competition":
                            return Color.FromHex("#3542a3");
                        case "kids and youth program":
                            return Color.FromHex("#f9db31");
                    }
                    return Color.PaleVioletRed;
                }
            }

            public ImageSource CategoryImage
            {
                get
                {
                    switch (Title.ToLowerInvariant())
                    {
                        case "main selection":
                            return "https://user-images.githubusercontent.com/20807086/62822618-81842680-bb75-11e9-9286-818af5b6433a.png";
                        case "newcomers":
                            return "https://user-images.githubusercontent.com/20807086/62822616-80eb9000-bb75-11e9-90cb-e841c9971e4f.png";
                        case "country in focus: germany":
                            return "https://user-images.githubusercontent.com/20807086/62822617-81842680-bb75-11e9-86f1-1e5c421d025d.png";
                        case "short dox":
                            return "https://user-images.githubusercontent.com/20807086/62822619-81842680-bb75-11e9-9d35-028e789df1d2.png";
                        case "student dox":
                            return "https://user-images.githubusercontent.com/20807086/62822620-81842680-bb75-11e9-9204-ff03c6222c76.png";
                    }
                    return null;
                }
            }
        }


        #endregion
    }
}
