﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class GuestServicePageModel : FreshMvvm.FreshBasePageModel
    {
        public override void Init(object initData)
        {
            base.Init(initData);
        }

        public GuestServicePageModel()
        {
            SetCommands();
        }

        public ICommand GoBack { get; set; }

        private void SetCommands()
        {
            GoBack = new Command(Back);
        }

        private void Back(object obj)
        {
            CoreMethods.PopPageModel();
        }
    }
}
