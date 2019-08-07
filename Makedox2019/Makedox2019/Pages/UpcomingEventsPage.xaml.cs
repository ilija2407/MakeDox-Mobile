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
	public partial class UpcomingEventsPage : ContentPage
	{

        public UpcomingEventsPage ()
		{
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
           // BottomBarPageExtensions.SetTabColor(this, Color.Yellow);
            InitializeComponent();
            
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