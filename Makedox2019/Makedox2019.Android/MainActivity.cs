using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using FFImageLoading.Forms.Platform;
using FFImageLoading;
using Plugin.LocalNotification;
using Android.Content;
using Acr.UserDialogs;

namespace Makedox2019.Droid
{
    [Activity(Label = "Makedox2019", Icon = "@drawable/logo", Theme = "@style/MyTheme.Splash", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);

            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
                Logger = new CustomLogger(),
            };
            ImageService.Instance.Initialize(config);
            UserDialogs.Init(this);

            NotificationCenter.CreateNotificationChannel();
            LoadApplication(new App());
            NotificationCenter.NotifyNotificationTapped(Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            NotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }

        protected override void OnStart()
        {
            base.OnStart();
            List<string> permissions = new List<string>();
            string permission = Manifest.Permission.AccessFineLocation;
            permissions.Add(Manifest.Permission.AccessCoarseLocation);
            permissions.Add(Manifest.Permission.AccessFineLocation);
            permissions.Add(Manifest.Permission.AccessLocationExtraCommands);
            permissions.Add(Manifest.Permission.AccessMockLocation);
            permissions.Add(Manifest.Permission.AccessNetworkState);
            permissions.Add(Manifest.Permission.AccessWifiState);
            permissions.Add(Manifest.Permission.Internet);

            if (ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, permissions.ToArray(), 0);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Permission Granted!!!");
            }
        }
    }
    public class CustomLogger : FFImageLoading.Helpers.IMiniLogger
    {
        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string errorMessage)
        {
            Console.WriteLine(errorMessage);
        }

        public void Error(string errorMessage, Exception ex)
        {
            Error(errorMessage + System.Environment.NewLine + ex.ToString());
        }
    }
}