using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Widget;
using Rg.Plugins.Popup.Extensions;
using WhereTo.Helpers;
using WhereTo.Models;
using WhereTo.Views;
using Xamarin.Forms;

namespace WhereTo.ViewModels
{
	public class SettingsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<User> Users { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public INavigation Navigation { get; set; }
        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }
        public SettingsViewModel()
        {
            Title = "Settings";
            User = new User
            {
                FirstName = "Demo",
                LastName = "Demo"
            };
            Users = new ObservableRangeCollection<User>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            AddCommand = new Command(async () =>
            {
                Console.WriteLine($"{User.FirstName} - {User.LastName}");
                Toast.MakeText(Forms.Context, $"Hello {User.FirstName}", ToastLength.Long).Show();
                Users.Add(User);
                var id = await UserDataStore.AddItemAsync(User);
                Application.Current.Properties["user_id"] = id;

                Application.Current.Properties["user_logged"] = true;
                await Navigation.PushModalAsync(App.GetMainPage());
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            /*
	        if (IsBusy)
	            return;

	        IsBusy = true;*/

            try
            {
                Users.Clear();
                var items = await UserDataStore.GetItemsAsync();
                Users.ReplaceRange(items.Distinct());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
