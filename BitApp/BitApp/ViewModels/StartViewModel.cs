using System;
using System.Collections.Generic;
using System.Text;
using BitApp.Models;
using BitApp.Services;
using System.Windows.Input;
using BitApp.Views;
using Xamarin.Forms;


namespace BitApp.ViewModels
{
    class StartViewModel
    {
        class StartPageViewModel : BaseViewModel
        {

            public StartPageViewModel()
            {
                ToLoginPageCommand = new Command(ToLoginPage);
                ToSignUpPageCommand = new Command(ToSignUpPage);
            }

            private void ToSignUpPage()
            {
                Push?.Invoke(new SignUpPage());
            }

            private void ToLoginPage()
            {
                Push?.Invoke(new LoginPage());
            }




            #region Properties


            #endregion

            #region Commands

            public ICommand ToLoginPageCommand { get; set; }

            public ICommand ToSignUpPageCommand { get; set; }

            #endregion

            #region Events

            public event Action<Page> Push;

            #endregion
        }
    }
}