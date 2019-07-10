﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class UpcomingEventsPageModel : FreshMvvm.FreshBasePageModel
    {
        #region Commands
        public ICommand NavigateToFilmsPageCommand { get; set; }
        #endregion

        #region CTOR
        public UpcomingEventsPageModel()
        {
            SetCommands();
        }

      

        public override void Init(object initData)
        {
            base.Init(initData);
        }

        #endregion


        #region Methods

        private void SetCommands()
        {
            NavigateToFilmsPageCommand = new Command(NavigateToFilmsPage);
        }

        private void NavigateToFilmsPage(object obj)
        {
            CoreMethods.PushPageModel<FilmsPageModel>(true);
        }


        #endregion
    }
}
