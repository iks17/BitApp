using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BitApp.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using BitApp;

namespace BitApp.Services
{
    class BitAPIProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string CLOUD_PHOTOS_URL = "TBD";
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:17978/BitAPI"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://192.168.1.14:17978/BitAPI"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "https://localhost:44326/BitAPI"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_PHOTOS_URL = "http://10.0.2.2:17978/Images/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_PHOTOS_URL = "http://192.168.1.14:17978/Images/"; //API url when using physucal device on android
        private const string DEV_WINDOWS_PHOTOS_URL = "https://localhost:44326/Images/"; //API url when using windoes on development

        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private static BitAPIProxy proxy = null;

        public static BitAPIProxy CreateProxy()
        {
            string baseUri;
            string basePhotosUri;
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                        basePhotosUri = DEV_ANDROID_EMULATOR_PHOTOS_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                        basePhotosUri = DEV_ANDROID_PHYSICAL_PHOTOS_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                    basePhotosUri = DEV_WINDOWS_PHOTOS_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
                basePhotosUri = CLOUD_PHOTOS_URL;
            }

            if (proxy == null)
                proxy = new BitAPIProxy(baseUri, basePhotosUri);
            return proxy;
        }


        private BitAPIProxy(string baseUri, string basePhotosUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
            this.basePhotosUri = basePhotosUri;
        }

        public string GetBasePhotoUri() { return this.basePhotosUri; }

        //Login!
        public async Task<User> LoginAsync(string email, string pass)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/Login?email={email}&pass={pass}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    User u = JsonSerializer.Deserialize<User>(content, options);
                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> SignUpAsync(User user)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler=ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                string json = JsonSerializer.Serialize<User>(user, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/SignUp", content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }



        // a function that checks that the inserted email and user name are unique

        public async Task<bool> CheckUnique(string email, string userName)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/CheckUniqueness?email={email}&userName={userName}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    bool isUnique = JsonSerializer.Deserialize<bool>(content, options);
                    return isUnique;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<double> GetTotalBalance()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetTotalBalance");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    double toatalBalance = JsonSerializer.Deserialize<double>(content, options);
                    return toatalBalance;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public async Task<bool> SendMoney(int amount, string phoneNumber)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/TransferMoney?amount={amount}&phoneNumber={phoneNumber}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<List<TransactionLog>> GetUserReceivedTransaction()
        {
            HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetUserReceivedTransaction");
            if(response.IsSuccessStatusCode)
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                    PropertyNameCaseInsensitive = true
                };
                string content = await response.Content.ReadAsStringAsync();
                List<TransactionLog> transactionLogs=JsonSerializer.Deserialize<List<TransactionLog>>(content,options);
                return transactionLogs;
            }
            else
            {
                return null;
            }

        }
        public async Task<bool> SendMoney(int amount)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/SendMoneyToMe?amount={amount}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
