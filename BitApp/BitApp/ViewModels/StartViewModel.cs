﻿using System;
using System.Collections.Generic;
using System.Text;
using BitApp.Models;
using BitApp.Services;
using System.Windows.Input;
using BitApp.Views;
using Xamarin.Forms;
using BitApp.ViewModels;

namespace BitApp.ViewModels
{
    class StartViewModel : BaseViewModel
    {

        public StartViewModel()
        {
            ToLoginPageCommand = new Command(ToLoginPage);
            ToSignUpPageCommand = new Command(ToSignUpPage);
        }

        private void ToSignUpPage()
        {
            Push?.Invoke(new SignUpPage()
            {
                BindingContext = new SignUpViewModel()
            }) ;
        }

        private void ToLoginPage()
        {
            Push?.Invoke(new LoginPage()
            {
                BindingContext = new LoginViewModel()
            }) ;
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