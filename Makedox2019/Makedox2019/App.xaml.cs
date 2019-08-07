using Acr.UserDialogs;
using FreshMvvm;
using Makedox2019.Effects;
using Makedox2019.PageModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Makedox2019
{
    public partial class App : Xamarin.Forms.Application
    {
        private static readonly Lazy<IUserDialogs> _userDialogs = new Lazy<IUserDialogs>(() => FreshIOC.Container.Resolve<IUserDialogs>());
        private static TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        public App()
        {
            FreshIOC.Container.Register<IUserDialogs>(UserDialogs.Instance);

            InitializeComponent();

            var tabbedNavigation = new FreshTabbedNavigationContainer();
            tabbedNavigation.AddTab<UpcomingEventsPageModel>("Home", "home_gray.png");
            tabbedNavigation.AddTab<TimelinePageModel>("Timeline", "timeline_gray.png");
            tabbedNavigation.AddTab<FilmsPageModel>("Films", "films_menu.png");
            tabbedNavigation.AddTab<MakedoxPlusPageModel>("Makedox+", "makedox_gray.png");
            tabbedNavigation.AddTab<MenuPageModel>("Menu", "menu_gray.png");
            tabbedNavigation.BarBackgroundColor = Color.Yellow;
            tabbedNavigation.Effects.Add(new BottomTabbarEffect());
            tabbedNavigation.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            tabbedNavigation.On<Android>().SetIsSwipePagingEnabled(false);
            tabbedNavigation.On<Android>().SetIsSmoothScrollEnabled(false);
            tabbedNavigation.On<Android>().SetIsLegacyColorModeEnabled(false);
            tabbedNavigation.UnselectedTabColor = Color.Gray;
            tabbedNavigation.SelectedTabColor = Color.White;

            MainPage = tabbedNavigation;
            var x = MainPage as Xamarin.Forms.TabbedPage;
            x.BarBackgroundColor = Color.FromHex("#f7b217");
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static async void ShowLoading(string title = "Loading..", MaskType? maskType = default(MaskType?))
        {
            await WaitInBackground();
            _userDialogs.Value.ShowLoading(title, maskType);
        }

        public static void HideLoading()
        {
            _userDialogs.Value.HideLoading();
        }

        public static IDisposable Loading(string title = "Loadin...", Action onCancel = default(Action), string cancelText = "Cancel", bool show = true, MaskType? maskType = default(MaskType?))
        {
            return _userDialogs.Value.Loading(title, onCancel, cancelText, show, maskType);
        }

        public static Task WaitInBackground()
        {
            return _tcs.Task;
        }

    }
}
