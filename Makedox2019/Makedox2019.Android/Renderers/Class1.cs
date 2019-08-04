using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ListView), typeof(Makedox2019.Droid.Renderers.CustomListViewRenderer))]
namespace Makedox2019.Droid.Renderers
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        public CustomListViewRenderer(Context ctx )
            : base(ctx)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement != null)
            {
                var control = this.Control as Android.Widget.ListView;
                control.NestedScrollingEnabled = true;
                
            }
        }
    }
}