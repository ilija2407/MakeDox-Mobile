using Acr.UserDialogs;
using Makedox2019.Controls;
using Makedox2019.Effects;
using Makedox2019.PageModels;
using Makedox2019.Pages;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Makedox2019
{
    public partial class App
    {
        private static readonly Lazy<IUserDialogs> _userDialogs = new Lazy<IUserDialogs>(() => UserDialogs.Instance);
        private static TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();
        /* 
        * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
        * This imposes a limitation in which the App class must have a default constructor. 
        * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
        */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }
        protected override async void OnInitialized()
        {

            InitializeComponent();

            await NavigationService.NavigateAsync($"NavigationPage/{nameof(MainPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageModel>();
            containerRegistry.RegisterForNavigation<UpcomingEventsPage, UpcomingEventsPageModel>();
            containerRegistry.RegisterForNavigation<TimelinePage, TimelinePageModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageModel>();
            containerRegistry.RegisterForNavigation<MakedoxPlusPage, MakedoxPlusPageModel>();
            containerRegistry.RegisterForNavigation<FilmsPage, FilmsPageModel>();
            containerRegistry.RegisterForNavigation<EventDetailsPage, EventDetailsPageModel>();
            containerRegistry.RegisterForNavigation<CategoryPage, CategoryPageModel>();
            containerRegistry.RegisterForNavigation<VenuesPage, VenuesPageModel>();
            containerRegistry.RegisterForNavigation<TicketsPage, TicketsPageModel>();
            containerRegistry.RegisterForNavigation<SocialPage, SocialPageModel>();
            containerRegistry.RegisterForNavigation<MapsPage, MapsPageModel>();
            containerRegistry.RegisterForNavigation<InfoPage, InfoPageModel>();
            containerRegistry.RegisterForNavigation<GuestServicePage, GuestServicePageModel>();
            containerRegistry.RegisterForNavigation<WorkshopsPage, WorkshopsPageModel>();
            containerRegistry.RegisterForNavigation<PhotoExebitionPage, PhotoExebitionPageModel>();
            containerRegistry.RegisterForNavigation<MusicPage, MusicPageModel>();
            containerRegistry.RegisterForNavigation<DocTalksPage, DocTalksPageModel>();
            containerRegistry.RegisterForNavigation<CoProPage, CoProPageModel>();
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


        //protected override void ConfigureViewModelLocator()
        //{
        //    base.ConfigureViewModelLocator();
        //    ViewModelLocationProvider.SetDefaultViewModelFactory((view, viewModelType) =>
        //    {
        //        var viewType = view.GetType();
        //        var vmFullName = viewType.FullName.Replace("Pages", "PageModels") + "Model";
        //        var vm = Container.Resolve(System.Reflection.Assembly.GetExecutingAssembly().GetType(vmFullName));
        //        return vm;
        //    });
        //}
    }
}
