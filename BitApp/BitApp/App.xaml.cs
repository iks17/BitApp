using BitApp.Models;
using BitApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BitApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv
        {
            get
            {
                return true; //change this before release!
            }
        }

       public User CurrentUser { get; set; }

        public App()
        {
            InitializeComponent();


            MainPage = new NavigationPage(new LoginPage());
        }
        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        
    }
}
