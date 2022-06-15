using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitApp.ViewModels;
using BitApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BitApp.Services;

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
        protected async override void OnAppearing()
        {
            SwapPageViewModel context = (SwapPageViewModel)this.BindingContext;
            BitAPIProxy proxy = BitAPIProxy.CreateProxy();
            List<TransactionLog> Logs = await proxy.GetUserReceivedTransaction();
            foreach (var item in Logs)
            {
                context.transactionLogs.Add(item);
            }
            base.OnAppearing();
        }
    }
}