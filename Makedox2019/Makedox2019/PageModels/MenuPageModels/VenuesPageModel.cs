using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Makedox2019.PageModels
{
    public class VenuesPageModel : FreshMvvm.FreshBasePageModel
    {
        public VenuesPageModel()
        {
            SetCommands();
        }

        public override void Init(object initData)
        {
            base.Init(initData);
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
