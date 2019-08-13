﻿using System;
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
    public partial class CollaborationPage : ContentPage
    {
        public CollaborationPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            InitializeComponent();
        }
    }
}