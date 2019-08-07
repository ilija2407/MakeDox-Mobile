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
using FFImageLoading;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Widget.AbsListView;

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
                control.ScrollStateChanged += (object sender, ScrollStateChangedEventArgs scrollArgs) => {
                    switch (scrollArgs.ScrollState)
                    {
                        case ScrollState.Fling:
                            ImageService.Instance.SetPauseWork(true); // all image loading requests will be silently canceled
                            break;
                        case ScrollState.Idle:
                            ImageService.Instance.SetPauseWork(false); // loading requests are allowed again
                            // Here you should have your custom method that forces redrawing visible list items
                            
                            break;
                    }
                };
            }
        }
    }
}