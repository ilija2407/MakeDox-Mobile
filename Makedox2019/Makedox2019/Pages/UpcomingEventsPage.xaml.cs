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
	public partial class UpcomingEventsPage : ContentPage
	{
        public int heightreg = 0;
        public UpcomingEventsPage ()
		{
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
           // BottomBarPageExtensions.SetTabColor(this, Color.Yellow);
            InitializeComponent();

       

        }


        void Handle_BindingContextChanged(object sender, System.EventArgs e)
        {
            var x = sender as CachedImage;

            var height = App.ScreenHeight;
            if (height > 800)
            {
                x.HeightRequest = 480;

            }
            else
            {
                x.HeightRequest = 380;
            }
            //throw new NotImplementedException();
        }
    }

    public class MovieListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AllTemplate { get; set; }
        public DataTemplate FavoritesTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((PageModels.UpcomingEventsPageModel.MovieLists)item).ShowAll ? AllTemplate : FavoritesTemplate;
        }
    }
}