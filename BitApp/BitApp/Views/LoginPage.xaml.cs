using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BitApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BitApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            LoginViewModel lVM = new LoginViewModel();
            BindingContext = lVM;
            lVM.Push += (p) => Navigation.PushAsync(p);
            //this.BackgroundImageSource = FileImageSource.FromFile("drawable/login.png");
        }
    }
}