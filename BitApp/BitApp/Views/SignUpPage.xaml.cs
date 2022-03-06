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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void Email_Unfocused(object sender, FocusEventArgs e)
        {
            SignUpViewModel viewModel=(SignUpViewModel)this.BindingContext ;
            if(!viewModel.ValidateEmail())
            {

            }
        }

        private void Password_Unfocused(object sender, FocusEventArgs e)
        {
            SignUpViewModel viewModel = (SignUpViewModel)this.BindingContext;
        
            if(!viewModel.ValidatePassword())
            {
                return;
            }
        }

        private void UserName_Unfocused(object sender, FocusEventArgs e)
        {
            UserEr.Text = "";
            SignUpViewModel viewModel = (SignUpViewModel)this.BindingContext;
            if (!viewModel.ValidateName())
            { 
                if (viewModel.UserName == "" || viewModel.UserName == null)
                {
                    UserEr.Text = "mandotry field";
                }
                UserEr.Text = "must contain a name";
            }

        }

        private void phone_Unfocused(object sender, FocusEventArgs e)
        {
            SignUpViewModel viewModel = (SignUpViewModel)this.BindingContext;

            if (!viewModel.ValidatePhoneNum())
            {
                return ;
            }
 
        }
    }
}