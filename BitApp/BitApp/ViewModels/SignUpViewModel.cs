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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BitApp.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        #region PropertyChangedEvent
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        #region Properties
        private bool isPrivateAccount;
        public bool IsPrivateAccount
        {
            get => isPrivateAccount;
            set
            {
                if (isPrivateAccount != value)
                {
                    isPrivateAccount = value;
                    OnPropertyChanged();
                }
            }
        }

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




        private string phoneNumberError;

        public string PhoneNumberError
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
        private string username;
        public string UserName { get=>username; set
            {
                if (value != username)
                {
                    username = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        #endregion
        public SignUpViewModel()
        {

            IsPrivateAccount = true;

            SignUpCommand = new Command(SignUp);

        }

        private async void SignUp()
        {
            BitAPIProxy proxy = BitAPIProxy.CreateProxy();
            try
            {//hi
              
                PhoneNumberError = "";
                EmailError = "";
                PasswordError = "";

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
                        UserName = username,
                        PhoneNumber = PhoneNumber,
                        Customers = new List<Customer>()
                    };

                    Customer customer = new Customer
                    {
                        //Address = string.Empty,
                        //Country=string.Empty,
                        // Gender=string.Empty,
                        //  FirstName=string.Empty,
                        //  UserName=user.UserName,


                        TransactionLogReceivers = new List<TransactionLog>(),
                        TransactionLogSenders = new List<TransactionLog>(),
                       




                    };
                    user.Customers.Add(customer);

                    if(IsPrivateAccount)
                    {
                        customer.PrivateAccounts = new List<PrivateAccount>();
                       PrivateAccount pa = new PrivateAccount
                        {
                            //AccountId="1",
                            // DateOfBirth=DateTime.Now,
            
                            TotalBalance = 0



                        };
                        customer.PrivateAccounts.Add(pa);
                    }
                    else
                    {
                        customer.BusinessAccounts = new List<BusinessAccount>();
                        BusinessAccount ba = new BusinessAccount()
                        {
                             //AccountId="1",
                             // BusibessAdress=string.Empty,
                             //  BusinessEmail=string.Empty,
                             //   BusinessName=string.Empty,
                             //    BusinessOpenDate=DateTime.Now,
                             //     BusinessPassword=string.Empty,
                                  
                            TotalBalance = 0,
                           
                        };
                        customer.BusinessAccounts.Add(ba);
                    }
                   
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
            EmailError = "";
            if(email==""||email==null)
            {
                EmailError = "mandatory field";
                return false;
            }
            try
            {
                MailAddress TestEmail = new MailAddress(Email);
            }
            catch (Exception)
            {
                EmailError = "email not valid";
                return false;
            }

            return true;
        }


        public bool ValidateName() //a function that checks that the inserted name has only letters
        {

            if(UserName==""||UserName==null)
                return false;
            Regex rg = new Regex("^[A-Z][a-zA-Z/s]+$");
            return rg.IsMatch(UserName);
        }

        public bool ValidatePhoneNum() //a function that checks that the inserted phone number is in the right format
        {
            PhoneNumberError = "";
            if(phoneNumber==""||phoneNumber==null)
            {
                PhoneNumberError = "mandatory field";
                return false;
            }
            Regex rg = new Regex("^05[0-9]{8}$");
            if(!rg.IsMatch(phoneNumber))
            PhoneNumberError = "must contain a phone number";
            return rg.IsMatch(PhoneNumber);
        }

        public bool ValidatePassword()
        {
            PasswordError = "";
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var input = Password;
            PasswordError = "";
           
            if (string.IsNullOrWhiteSpace(input))
            {
                this.PasswordError = "Mamdatory field";
                return false;
            }

            if (!(input.Length >= 6 && input.Length <= 12))
            {
                PasswordError += "Password should not be less than 6 characters and greater than 12 characters. ";
                
                return false;
            }
            if (!hasNumber.IsMatch(input))
            {
                PasswordError += "Password should contain At least one number. ";
                
                return false;
            }
            if (!hasLowerChar.IsMatch(input))
            {
                PasswordError += "Password should contain At least one lower case letter. ";
              
                return false;

            }
            if (input.Contains(" "))
            {
                PasswordError += "Password should not contain space. ";
                
                return false;
            }
            if (!hasUpperChar.IsMatch(input))
            {
                PasswordError += "Password should contain At least one upper case letter.";
                
                return false;
            }
            return true;
            
        }
        


        #region Commands

        public ICommand SignUpCommand { get; set; }

        #endregion

        #region Events


        #endregion

    }
}
