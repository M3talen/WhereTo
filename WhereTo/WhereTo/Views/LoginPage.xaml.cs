using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Widget;
using Java.Lang;
using WhereTo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Application = Android.App.Application;
using Thread = System.Threading.Thread;

namespace WhereTo.Views
{

    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this;


            FacebookLoginCommand = new Command(FacebookLogin);
            GoogleLoginCommand = new Command(GoogleLogin);


            InitializeComponent();
        }


        public async Task LoginSuccess()
        {
            try
            {
                IsBusy = true;

                await Navigation.PushModalAsync(App.GetMainPage());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void GoogleLogin()
        {
            if (IsBusy) return;
           
            
            Android.Accounts.AccountManager account = Android.Accounts.AccountManager.Get(Application.Context);
            Android.Accounts.Account[] tabAccount = account.GetAccountsByType("com.google");
            List<Android.Accounts.Account> list = new List<Android.Accounts.Account>();
            foreach (Android.Accounts.Account act in tabAccount)
            {
                string name = act.Name;
                list.Add(act); Toast.MakeText(Forms.Context, "Hello" +name , ToastLength.Long).Show();

            }


            IsBusy = true;
            await LoginSuccess();
        }

        private async void FacebookLogin()
        {
            if (IsBusy) return;
            Toast.MakeText(Forms.Context, "Hello", ToastLength.Long).Show();
            IsBusy = true;
            await LoginSuccess();
        }

        public ICommand FacebookLoginCommand { get; }

        public ICommand GoogleLoginCommand { get; }
    }
}
