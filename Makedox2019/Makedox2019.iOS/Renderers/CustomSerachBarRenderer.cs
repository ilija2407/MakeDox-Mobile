using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Makedox2019.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSerachBarRenderer))]
namespace Makedox2019.iOS.Renderers
{
    public class CustomSerachBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            UITextField.AppearanceWhenContainedIn(typeof(UISearchBar)).BackgroundColor = UIColor.Clear;
        }
    }
}