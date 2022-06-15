﻿using System;
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

namespace BitApp.ViewModels
{
    public class SwapPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string phonenumber;
        public string PhoneNumber
        {
            get => phonenumber; set
            {
                if (phonenumber != value)
                {
                    phonenumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }
        private string amountStr;
        public string AmountStr { get=>amountStr; set 
            {
                if(amountStr != value)
                {
                    amountStr = value;
                    OnPropertyChanged(nameof(AmountStr));
                }
            } }
        public ICommand transferCommand => new Command(transferMoney);
        public async void transferMoney()
        {
            try 
            { 
                BitAPIProxy proxy = BitAPIProxy.CreateProxy();
                if(amountStr==""|| amountStr == " " || amountStr == null)
                {
                    //show error with value
                    return;
                }
                int amount = int.Parse(AmountStr);
                bool con = await proxy.SendMoney(amount,PhoneNumber) ;
            }
            catch(Exception e)
            {
                //shoe error message
            }



        }
    }
}
