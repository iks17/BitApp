using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BitApp.ViewModels;

namespace BitApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            StartViewModel sPVM = new StartViewModel();
            BindingContext = sPVM;
            sPVM.Push += (p) => Navigation.PushAsync(p);
        }
    }
}