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
            await Navigation.PushModalAsync(App.GetMainPage());
        }

	    private  void GoogleLogin()
	    {
	        Toast.MakeText(Forms.Context, "Hello", ToastLength.Long).Show();
            LoginSuccess();
	    }

	    private  void FacebookLogin()
	    {
	        Toast.MakeText(Forms.Context, "Hello", ToastLength.Long).Show();
             LoginSuccess();
        }

	    public ICommand FacebookLoginCommand { get; }

	    public ICommand GoogleLoginCommand { get; }
    }
}
