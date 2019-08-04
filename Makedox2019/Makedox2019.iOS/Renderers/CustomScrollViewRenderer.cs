using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Makedox2019.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ScrollView), typeof(CustomScrollViewRenderer))]
namespace Makedox2019.iOS.Renderers
{
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if(e.NewElement != null)
            {
                this.Bounces = false;
            }
        }
    }
}