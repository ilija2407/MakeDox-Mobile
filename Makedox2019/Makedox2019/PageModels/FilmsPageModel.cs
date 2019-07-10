using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class FilmsPageModel : FreshMvvm.FreshBasePageModel
    {
        #region Commands
        public ICommand NavigateToUpcommingEventsPageCommand { get; set; }
        #endregion

        public FilmsPageModel()
        {
            SetCommands();
        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }

        #region Methods

        private void SetCommands()
        {
            NavigateToUpcommingEventsPageCommand = new Command(NavigateToUpcommingEventsPage);
        }

        private void NavigateToUpcommingEventsPage(object obj)
        {
            CoreMethods.PushPageModel<UpcomingEventsPageModel>(true);
        }


        #endregion
    }
}
