using System;
using Android.Content;
using Android.Graphics;
using Makedox2019.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace Makedox2019.Droid.Renderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        public CustomLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement != null)
            {
                Control.Typeface = e.NewElement.FontAttributes == FontAttributes.Bold ? Typeface.CreateFromAsset(this.Context.Assets, "Intro-Bold.otf") : Typeface.CreateFromAsset(this.Context.Assets, "Intro-Regular.otf");
                Control.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)e.NewElement.FontSize);
            }
        }
    }
}