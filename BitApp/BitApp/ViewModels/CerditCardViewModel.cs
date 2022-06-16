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
    public class CerditCardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string name;
        public string Name { get=>name; set
            {
                if(name!=value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            } }
        private string cardnum;
        public string CardNum { get=>cardnum; set 
            {
                if(value!=cardnum)
                {
                    cardnum = value;
                    OnPropertyChanged(nameof(CardNum));
                }
            } }
        private string cvv;
        public string CVV { get => cvv; set 
            { 
            if(cvv!=value)
                {
                    cvv = value;
                    OnPropertyChanged(nameof(CVV));
                }
            } }
        private DateTime date;
        public DateTime Date { get=>date; set 
            {
                if(date!=value)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
                }
            } }
        public ICommand AddCreditCardCommand => new Command(AddCreditCard);
        public async void AddCreditCard()
        {
            Card card = new Card()
            {
                Name=this.Name,
                Id=this.CardNum,
                Date=this.Date,
                Cvc=this.CVV,
                UserId=((App)App.Current).CurrentUser.UserId
            };
            BitAPIProxy proxy = BitAPIProxy.CreateProxy();
            bool con = await proxy.AddCreditCard(card);
            if(con)
            {
                await App.Current.MainPage.DisplayAlert("Succes!", $" The crad has been add ", "ok");

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Faild!", $" We couldnt add the card ", "ok");

            }
        }

    }
}
