using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Makedox2019.iOS.Renderers;
[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace Makedox2019.iOS.Renderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var q = UIKit.UIFont.FamilyNames;
            if (e.NewElement != null)
                Control.Font = e.NewElement.FontAttributes == FontAttributes.Bold ? UIKit.UIFont.FromName("Intro Bold", (nfloat)e.NewElement.FontSize) : UIKit.UIFont.FromName("Intro Regular", (nfloat)e.NewElement.FontSize);
        }
    }
}
    