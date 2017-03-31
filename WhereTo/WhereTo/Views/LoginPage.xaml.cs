using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Widget;
using WhereTo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhereTo.Views
{

	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
		    BindingContext = this;


		    FacebookLoginCommand = new Command(FacebookLogin);
		    GoogleLoginCommand = new Command(GoogleLogin);


		    InitializeComponent();
        }

        public async Task LoginSuccess()
        {
            if (!IsBusy)
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
        }

	    private async void GoogleLogin()
	    {
            if(IsBusy) return;
            Toast.MakeText(Forms.Context, "Hello", ToastLength.Long).Show();
            await LoginSuccess();
	    }

	    private async void FacebookLogin()
	    {
	        if (IsBusy) return;
            Toast.MakeText(Forms.Context, "Hello", ToastLength.Long).Show();
            await LoginSuccess();
        }

	    public ICommand FacebookLoginCommand { get; }

	    public ICommand GoogleLoginCommand { get; }
    }
}
