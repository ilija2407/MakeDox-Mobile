using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Makedox2019.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailsPage : ContentPage
    {
        public EventDetailsPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            InitializeComponent();
            var height = App.ScreenHeight;
            if (height > 700)
            {
                productImg.HeightRequest = 400;
                productImgBorder.HeightRequest = 402;

                // vendorProductList.Margin = new Thickness(20, 5, 20, 70);
                // myProductsList.Margin = new Thickness(0, 60, 0, 0);  
            }
            else
            {
                productImg.HeightRequest = 300;
                productImgBorder.HeightRequest = 302;
                //vendorProductList.Margin = new Thickness(20, 5, 20, 70);
            }
        }
    }
}