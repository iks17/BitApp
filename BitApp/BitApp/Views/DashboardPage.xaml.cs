using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitApp.Services;
using BitApp.ViewModels;
using BitApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BitApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            this.BindingContext = new DashboardPageViewModel();
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            DashboardPageViewModel contex = (DashboardPageViewModel)this.BindingContext;
            BitAPIProxy proxy = BitAPIProxy.CreateProxy();
             contex.Card= await proxy.HasCreditCard();
            MoveMoney.IsVisible = contex.Card!=null;
            if(contex.Card==null)
            {
                contex.Card = new Card()
                {
                    Name = "Name",
                    Id = "1234"
                };
            }
            base.OnAppearing();
            ((DashboardPageViewModel)this.BindingContext).OnAppearing();
        }
    }
}