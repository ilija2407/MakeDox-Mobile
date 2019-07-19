using System;
using CoreGraphics;
using Makedox2019.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), 
typeof(CustomTabbedPageRenderer))]
namespace Makedox2019.iOS.Renderers
{
    class CustomTabbedPageRenderer : TabbedRenderer
        {
        // Modify this variable with the height you desire.
        private readonly float tabBarHeight = 70f;

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            TabBar.Frame = new CGRect(TabBar.Frame.X, TabBar.Frame.Y + (TabBar.Frame.Height - tabBarHeight), TabBar.Frame.Width, tabBarHeight);
        }
    }
    }