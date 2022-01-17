using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using BitApp.Models;
using BitApp.Services;
using BitApp.Views;

namespace BitApp.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        public SignUpViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;
            PhoneNumber = string.Empty;          
            status = string.Empty;
            EmailError = string.Empty;
            PasswordError = string.Empty;
            PhoneNumberError = default(int);

            SignUpCommand = new Command(SignUp);

        }

        private async void SignUp()
        {
            BitAPIProxy proxy = BitAPIProxy.CreateProxy();
            try
            {//hi
              
                PhoneNumberError = 0;
                EmailError = string.Empty;
                PasswordError = string.Empty;

                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(PhoneNumber)
                    || string.IsNullOrEmpty(Password) || !ValidateEmail() )
                {

             if (string.IsNullOrEmpty(Email))
                        EmailError = "Required Field";
                    else if (!ValidateEmail())
                        EmailError = "Email Not Valid";             

                    if (string.IsNullOrEmpty(Password))
                        PasswordError = "Required Field";

                    await App.Current.MainPage.DisplayAlert("Failed", "Fail!!!!", "ok");
                    return;
                }
                bool isUnique = await proxy.CheckUnique(Email, Password);

                if (isUnique)
                {

                    User user = new User
                    {
                        Email = Email,                       
                        Password = Password,                  
                        PhoneNumber = PhoneNumber,
                  
                        Admins = new List<Admin>(),
                        Customers = new List<Customer>()
                    };



                    bool b = await proxy.SignUpAsync(user);
                    Status = "Signing you up...";
                    if (b)
                    {
                        Status = "Sign Up Completed:)";
                        await App.Current.MainPage.DisplayAlert("Success", Status, "ok");
                    }

                    else
                    {
                        Status = "Something Went Wrong...";
                        await App.Current.MainPage.DisplayAlert("Failed", Status, "ok");
                    }

                }
                else
                {
                    Status = "Email or/and User Name has/have already been used";
                    await App.Current.MainPage.DisplayAlert("Failed", Status, "ok");
                }


            }
            catch (Exception)
            {
                Status = "Something Went Wrong...";
            }
        }

        public bool ValidateEmail() // a function that checks that the inserted email is in an email format
        {
            try
            {
                MailAddress TestEmail = new MailAddress(Email);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public bool ValidateName(string name) //a function that checks that the inserted name has only letters
        {
            Regex rg = new Regex("^[A-Z][a-zA-Z/s]+$");
            return rg.IsMatch(name);
        }

        public bool ValidatePhoneNum() //a function that checks that the inserted phone number is in the right format
        {
            Regex rg = new Regex("^05[0-9]{8}$");
            return rg.IsMatch(PhoneNumber);
        }


        #region Properties
        private string email;

        public string Email
        {
            get => email;
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                if (value != password)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        private string status;

        public string Status
        {
            get => status;
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged();
                }
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                if (value != phoneNumber)
                {
                    phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }




        private int phoneNumberError;

        public int PhoneNumberError
        {
            get => phoneNumberError;
            set
            {
                if (value != phoneNumberError)
                {
                    phoneNumberError = value;
                    OnPropertyChanged();
                }
            }
        }
       
        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                if (value != emailError)
                {
                    emailError = value;
                    OnPropertyChanged();
                }
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                if (value != passwordError)
                {
                    passwordError = value;
                    OnPropertyChanged();
                }
            }
        }
        private string statusError;

        public string StatusError
        {
            get => statusError;
            set
            {
                if (value != statusError)
                {
                    statusError = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands

        public ICommand SignUpCommand { get; set; }

        #endregion

        #region Events


        #endregion

    }
}
