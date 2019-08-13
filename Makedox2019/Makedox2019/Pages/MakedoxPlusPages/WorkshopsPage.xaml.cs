using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Makedox2019.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkshopsPage : ContentPage
	{
		public WorkshopsPage ()
		{
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            InitializeComponent();
		}
	}
}