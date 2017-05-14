using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Widget;
using Rg.Plugins.Popup.Extensions;
using WhereTo.Models;
using WhereTo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhereTo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserSetupPage : ContentPage
	{
	    public SettingsViewModel _viewModel;
	   
        public UserSetupPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);



	        BindingContext = _viewModel = new SettingsViewModel();
            _viewModel.Navigation = Navigation;
        }
        
        
	}
}
