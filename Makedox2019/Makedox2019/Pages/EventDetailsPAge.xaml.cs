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
        }
    }
}