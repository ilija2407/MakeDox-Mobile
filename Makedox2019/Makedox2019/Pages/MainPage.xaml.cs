using Makedox2019.Effects;
using Makedox2019.PageModels;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Makedox2019.Pages
{
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            Children.Add(new NavigationPage(new Pages.UpcomingEventsPage())
            {
                Title = "Home",
                IconImageSource = "home_gray.png"
            }); Children.Add(new NavigationPage(new Pages.TimelinePage())
            {
                Title = "Timeline",
                IconImageSource = "timeline_gray.png"
            }); Children.Add(new NavigationPage(new Pages.FilmsPage())
            {
                Title = "Films",
                IconImageSource = "films_menu.png"
            }); Children.Add(new NavigationPage(new Pages.MakedoxPlusPage())
            {
                Title = "Makedox+",
                IconImageSource = "makedox_gray.png"
            }); Children.Add(new NavigationPage(new Pages.MenuPage())
            {
                Title = "Menu",
                IconImageSource = "menu_gray.png"
            });
            this.BarBackgroundColor = Color.FromHex("#f7b217");
            this.Effects.Add(new BottomTabbarEffect());
            this.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            this.On<Android>().SetIsSwipePagingEnabled(false);
            this.On<Android>().SetIsSmoothScrollEnabled(false);
            this.On<Android>().SetIsLegacyColorModeEnabled(false);
            this.UnselectedTabColor = Color.Gray;
            this.SelectedTabColor = Color.White;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(CurrentPage) && CurrentPage != null)
            {
                if ((CurrentPage as NavigationPage)?.CurrentPage?.BindingContext is ViewModelBase vm)
                    vm.OnNavigatedTo(null);

                if(CurrentPage.BindingContext is ViewModelBase vms)
                    vms.OnNavigatedTo(null);
            }
        }
    }
}
