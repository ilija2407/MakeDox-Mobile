using Acr.UserDialogs;
using BottomBar.XamarinForms;
using FreshMvvm;
using Makedox2019.Controls;
using Makedox2019.Models;
using Makedox2019.PageModels;
using Makedox2019.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Plugin.LocalNotification;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
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
            
            if (Device.RuntimePlatform == Device.iOS)
            {
                var tabbedNavigation = new FreshTabbedNavigationContainer();
                tabbedNavigation.AddTab<UpcomingEventsPageModel>("Home", "home_gray.png");
                tabbedNavigation.AddTab<TimelinePageModel>("Timeline", "timeline_gray.png");
                tabbedNavigation.AddTab<FilmsPageModel>("Films", "films_menu.png");
                tabbedNavigation.AddTab<MakedoxPlusPageModel>("Makedox+", "makedox_gray.png");
                tabbedNavigation.AddTab<MenuPageModel>("Menu", "menu_gray.png");
                tabbedNavigation.BarBackgroundColor = Color.Yellow;
                //tabbedNavigation.AddTab<UpcomingEventsPageModel>(null, "home_gray.png");
                //tabbedNavigation.AddTab<TimelinePageModel>(null, "timeline_gray.png");
                //tabbedNavigation.AddTab<FilmsPageModel>(null, "films_gray.png");
                //tabbedNavigation.AddTab<MakedoxPlusPageModel>(null, "makedox_gray.png");
                //tabbedNavigation.AddTab<MenuPageModel>(null, "menu_gray.png");
                MainPage = tabbedNavigation;
                var x = MainPage as Xamarin.Forms.TabbedPage;
                x.BarBackgroundColor = Color.FromHex("#f7b217");
            }
            else
            {
                var bottomBarPage = new CustomNavigation() { BarTextColor = Color.White, BarBackgroundColor = Color.White };

                //:BottomBarPageExtensions.TabColor="#FF5252"


                bottomBarPage.FixedMode = false;
                bottomBarPage.BarTextColor = Color.White;
                bottomBarPage.AddTab<UpcomingEventsPageModel>("Home", "home.png");
                bottomBarPage.AddTab<TimelinePageModel>("Timeline", "timeline.png");
                bottomBarPage.AddTab<FilmsPageModel>("Films", "films.png");
                bottomBarPage.AddTab<MakedoxPlusPageModel>("Makedox+", "makedox.png");
                bottomBarPage.AddTab<MenuPageModel>("Menu", "menu.png");

                MainPage = bottomBarPage;
            }

            //var page = FreshPageModelResolver.ResolvePageModel<UpcomingEventsPageModel>();
            //var basicNavContainer = new FreshNavigationContainer(page);
            //MainPage = basicNavContainer;

           
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
