using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BitApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwapPage : ContentPage
    {
        public SwapPage()
        {
            SwapPageViewModel context = new SwapPageViewModel();
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}