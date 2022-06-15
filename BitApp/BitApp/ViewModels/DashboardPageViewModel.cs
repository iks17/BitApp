using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using Xamarin.Forms.PlatformConfiguration;
using BitApp.Services;
using BitApp.Models;

namespace BitApp.ViewModels
{
    class DashboardPageViewModel : BaseViewModel
    {
        private Customer customer;
        private double totalBalance;
        public double TotalBalance
        {
            get { return totalBalance; }
            set
            {
                if(totalBalance != value)
                {
                    totalBalance = value;
                    OnPropertyChanged(nameof(TotalBalance));
                }
            }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if(userName != value)
                {
                    userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        public User user;
        public  event Action OnAppearingEvent;
        public DashboardPageViewModel()
        {
            user= ((App)App.Current).CurrentUser;
            UserName = ((App)App.Current).CurrentUser.UserName;
            customer = ((App)App.Current).CurrentUser.Customers.FirstOrDefault();

            ErrorMessage = "";
            OnAppearingEvent += TotalBalanceUpdate;
        }
        private async void TotalBalanceUpdate()
        {
            BitAPIProxy proxy = BitAPIProxy.CreateProxy();
           TotalBalance =  await proxy.GetTotalBalance();
        }
        public void OnAppearing()
        {
            OnAppearingEvent?.Invoke();
        }
        private string amountStr;
        public string AmountStr
        {
            get => amountStr; set
            {
                if (amountStr != value)
                {
                    amountStr = value;
                    OnPropertyChanged(nameof(AmountStr));
                }
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage; set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }
        public ICommand MoneyToMe => new Command(sendMoney);
        public async void sendMoney()
        {
            try
            {
                BitAPIProxy proxy = BitAPIProxy.CreateProxy();
                if (amountStr == "" || amountStr == " " || amountStr == null )
                {
                    ErrorMessage = "plese entar valid amount and valid hole number";
                    return;
                }
                int amount = int.Parse(AmountStr);
                bool con = await proxy.SendMoney(amount);
                if (con)
                {
                    await App.Current.MainPage.DisplayAlert("Succes!", $"{amount} dollars have been transferd to ", "ok");
                    AmountStr = "";
                    
                }
                else
                {
                    ErrorMessage = "somthing went wrong";
                    return;
                }
            }
            catch (Exception e)
            {
                ErrorMessage = "somthing went wrong";
                return;
            }
        }
    }
}
