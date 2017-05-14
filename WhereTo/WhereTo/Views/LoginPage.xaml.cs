using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Widget;
using Java.Lang;
using WhereTo.Helpers;
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
            TitterLoginCommand = new Command(TwitterLogin);

            InitializeComponent();
        }

        private async void TwitterLogin()
        {

            if (IsBusy) return;
            IsBusy = true;

            if (OAuthConfig.User == null)
            {
                await Navigation.PushModalAsync(new ProviderLoginPage("Twitter"));
            }
        }


        public async Task LoginSuccess()
        {
            try
            {
                IsBusy = true;
                var id = Xamarin.Forms.Application.Current.Properties["user_id"] as int?;
                if (id != null && id.Value == 0)
                    await Navigation.PushModalAsync(new NavigationPage(new UserSetupPage()));
                else
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
            IsBusy = true;
            await LoginSuccess();
        }

        private async void FacebookLogin()
        {
            if (IsBusy) return;
            IsBusy = true;

            if (OAuthConfig.User == null)
            {
                await Navigation.PushModalAsync(new ProviderLoginPage("FaceBook"));
            }
            
            await LoginSuccess();
        }

        public ICommand FacebookLoginCommand { get; }

        public ICommand GoogleLoginCommand { get; }

        public ICommand TitterLoginCommand { get; }
    }
}
