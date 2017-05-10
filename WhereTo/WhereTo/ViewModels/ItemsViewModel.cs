using System;
using System.Diagnostics;
using System.Threading.Tasks;

using WhereTo.Helpers;
using WhereTo.Models;
using WhereTo.Views;

using Xamarin.Forms;

namespace WhereTo.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Event> Events { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Events = new ObservableRangeCollection<Event>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Events.Clear();
                var lat = Application.Current.Properties["lat"] as double?;
                var longi = Application.Current.Properties["long"] as double?;
                var radius = Application.Current.Properties["radius"] as double?;
                var items = await DataStore.GetItemsAsync(longi ?? 0, lat ?? 0,radius ?? 0);
                Events.ReplaceRange(items);
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