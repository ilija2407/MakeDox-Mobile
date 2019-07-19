﻿using BottomBar.XamarinForms;
using FreshMvvm;
using Makedox2019.Controls;
using Makedox2019.PageModels;
using Makedox2019.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Makedox2019
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
            {
                var tabbedNavigation = new FreshTabbedNavigationContainer();
                tabbedNavigation.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
                tabbedNavigation.AddTab<UpcomingEventsPageModel>(null, "home.png");
                tabbedNavigation.AddTab<TimelinePageModel>(null, "timeline.png");
                tabbedNavigation.AddTab<FilmsPageModel>(null, "films.png");
                tabbedNavigation.AddTab<MakedoxPlusPageModel>(null, "makedox.png");
                tabbedNavigation.AddTab<MenuPageModel>(null, "menu.png");
                MainPage = tabbedNavigation;
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
    }
}
