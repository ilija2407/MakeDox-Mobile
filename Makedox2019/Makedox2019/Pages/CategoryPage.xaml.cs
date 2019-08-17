using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Makedox2019.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            InitializeComponent();
        }

        void Handle_BindingContextChanged(object sender, System.EventArgs e)
        {
            var x = sender as CachedImage;

            var height = App.ScreenHeight;
            if (height > 800)
            {
                x.HeightRequest = 400;

            }
            else
            {
                x.HeightRequest = 300;
            }
        }
    }
}