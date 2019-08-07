using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Makedox2019.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Makedox2019")]
[assembly: ExportEffect(typeof(BottomTabbarEffect), "BottomTabbarEffect")]
namespace Makedox2019.Droid.Effects
{
    public class BottomTabbarEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (!(Container.GetChildAt(0) is ViewGroup layout))
                return;

            if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
                return;

            // This is what we set to adjust if the shifting happens
            bottomNavigationView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityLabeled;
            bottomNavigationView.ItemTextAppearanceActive = ResourceManager.GetStyle(bottomNavigationView.Context, "TextAppearance.BottomNavigationView.Active");
            bottomNavigationView.ItemTextAppearanceInactive = ResourceManager.GetStyle(bottomNavigationView.Context, "TextAppearance.BottomNavigationView.Inactive");
        }

        protected override void OnDetached()
        {
        }
    }
}