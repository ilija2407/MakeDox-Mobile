using Makedox2019.Pages;
using PanCardView.Extensions;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class WorkshopsPageModel : ViewModelBase, INotifyPropertyChanged
    {
        public ObservableCollection<Image> Items { get; set; }
        public ICommand PanPositionChangedCommand { get; }
        public ICommand ImgTapped { get; set; }
        public ICommand RemoveCurrentItemCommand { get; }
        private int _currentIndex;

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;

                //base.OnPropertyChanged(_currentIndex);
                    RaisePropertyChanged(nameof(CurrentIndex));


                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentIndex)));
            }
        }
        public bool IsAutoAnimationRunning { get; set; }

        public bool IsUserInteractionRunning { get; set; }
        public WorkshopsPageModel(INavigationService navigationService)
            : base(navigationService)
        {
            SetCommands();

            Items = new ObservableCollection<Image>
            {
                new Image{Source="jana.png"},
                new Image{Source="victor.png"},
            };
            var list = Items;

            PanPositionChangedCommand = new Command(v =>
            {
                if (IsAutoAnimationRunning || IsUserInteractionRunning)
                {
                    return;
                }

                var index = CurrentIndex + (bool.Parse(v.ToString()) ? 1 : -1);
                if (index < 0 || index >= Items.Count)
                {
                    return;
                }
                CurrentIndex = index;
            });

            RemoveCurrentItemCommand = new Command(() =>
            {
                if (!Items.Any())
                {
                    return;
                }
                Items.RemoveAt(CurrentIndex.ToCyclingIndex(Items.Count));
            });
        }

        public ICommand GoBack { get; set; }

        private void SetCommands()
        {
            GoBack = new Command(Back);
            ImgTapped = new Command(ImgTappedAction);
        }

        private async void ImgTappedAction(object obj)
        {
            var param = obj as Image;
            var source = param.Source;

            if (source.ToString().Contains("jana.png"))
            {
                await _navigationService.NavigateAsync($"{nameof(CategoryPage)}?Category=WorkshopsJana");
            }
            else
            {
                 await _navigationService.NavigateAsync($"{nameof(CategoryPage)}?Category=WorkshopsVictor");
            }
            //throw new NotImplementedException();
        }

        private void Back(object obj)
        {
            _navigationService.GoBackAsync();
        }

    }
}
