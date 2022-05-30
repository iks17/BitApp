using System;
using System.Collections.Generic;
using System.Text;
using BitApp.Services;
using System.Linq;
using BitApp.Models;

namespace BitApp.ViewModels
{
    class HomeViewModel : BaseViewModel
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }
        public  event Action OnAppearingEvent;
        public HomeViewModel()
        {
            UserName = ((App)App.Current).CurrentUser.UserName;
            customer = ((App)App.Current).CurrentUser.Customers.FirstOrDefault();
           
            
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
    }
}
