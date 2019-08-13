using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Makedox2019.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapsPage : ContentPage
	{
        Pin currentPin;
        public MapsPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            InitializeComponent();
            InitializeMakedoxPins();
            InitRecommendedPins();
            MoveMap();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }

        void MoveMap()
        {
            Xamarin.Forms.GoogleMaps.Position xamPos = new Xamarin.Forms.GoogleMaps.Position(42.003724, 21.435270);
            googleMap.MoveToRegion(MapSpan.FromCenterAndRadius(xamPos, Distance.FromMiles(0.6)));
        }

        void InitializeMakedoxPins()
        {
            var icon = BitmapDescriptorFactory.FromView(new Image
            {
                Source = "ic_makedox_stamp.png",
                HeightRequest = 35,
                WidthRequest = 35
            });
            var pin1 = new Pin()
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.GoogleMaps.Position(42.003073, 21.437015),
                Label = "Kurshumli An",
                Icon = icon
            };
            var pin2 = new Pin()
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.GoogleMaps.Position(42.002386, 21.436201),
                Label = "Kurshumli Out",
                Icon = icon
            };
            var pin3 = new Pin()
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.GoogleMaps.Position(42.003961, 21.432711),
                Label = "Museum of Contemporary Art Skopje (MOCA)",
                Icon = icon
            };


            googleMap.Pins.Add(pin1);
            googleMap.Pins.Add(pin2);
            googleMap.Pins.Add(pin3);
        }
        void InitRecommendedPins()
        {
            var pin1 = new Pin()
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.GoogleMaps.Position(42.000953, 21.438113),
                Label = "MakeDox recommends:",
                Address = "Galerija 7"
            };
            var pin2 = new Pin()
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.GoogleMaps.Position(42.001252, 21.423275),
                Label = "MakeDox recommends:",
                Address = "Fourplay Cafe"
            };
            var pin3 = new Pin()
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.GoogleMaps.Position(41.998809, 21.425333),
                Label = "MakeDox recommends:",
                Address = "Vinyl"
            };
            var pin4 = new Pin()
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.GoogleMaps.Position(41.999423, 21.435810),
                Label = "MakeDox recommends:",
                Address = "Menada"
            };
            googleMap.Pins.Add(pin1);
            googleMap.Pins.Add(pin2);
            googleMap.Pins.Add(pin3);
            googleMap.Pins.Add(pin4);
        }
        ContentView CreateMakedoxIcon()
        {
            ContentView contentView = new ContentView();
            contentView.HeightRequest = 30;
            contentView.WidthRequest = 30;
            contentView.IsClippedToBounds = false;
            contentView.Content = new Image
            {
                Source = "ic_makedox_stamp.png",
                HeightRequest = 30,
                WidthRequest = 30
            };

            return contentView;
        }


        void GoogleMap_InfoWindowClicked(object sender, InfoWindowClickedEventArgs e)
        {
            string destination = e.Pin.Position.Latitude + "," + e.Pin.Position.Longitude;
            var urlSufix = "&daddr=" + destination;
            if (Device.RuntimePlatform == Device.iOS)
            {
                Device.OpenUri(new Uri("http://maps.apple.com/?saddr=Current%20Location" + urlSufix));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Device.OpenUri(new Uri("http://maps.google.com/?saddr=" + urlSufix));
            }
        }

        private void goBtn_Clicked(object sender, EventArgs e)
        {
            if (currentPin == null) return;
            string destination = currentPin.Position.Latitude + "," + currentPin.Position.Longitude;
            var urlSufix = "&daddr=" + destination;
            if (Device.RuntimePlatform == Device.iOS)
            {
                Device.OpenUri(new Uri("http://maps.apple.com/?saddr=Current%20Location" + urlSufix));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Device.OpenUri(new Uri("http://maps.google.com/?saddr=" + urlSufix));
            }
        }

        private void googleMap_PinClicked(object sender, PinClickedEventArgs e)
        {
            currentPin = e.Pin;
            goBtn.IsVisible = true;

        }

        private void googleMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            currentPin = null;
            goBtn.IsVisible = false;

        }
        //async Task StartListening()
        //{
        //    if (CrossGeolocator.Current.IsListening)
        //        return;
        //    CrossGeolocator.Current.DesiredAccuracy = 30;
        //    if(Device.RuntimePlatform == Device.iOS) {
        //        await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(10), 0);
        //    }
        //    await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(10), 10, false, new Plugin.Geolocator.Abstractions.ListenerSettings
        //    {
        //        AllowBackgroundUpdates = true,
        //        ListenForSignificantChanges = false,
        //        PauseLocationUpdatesAutomatically = false
        //    });

        //    //CrossGeolocator.Current.PositionChanged += PositionChanged;
        //    //CrossGeolocator.Current.PositionError += PositionError;
        //}
    }

}