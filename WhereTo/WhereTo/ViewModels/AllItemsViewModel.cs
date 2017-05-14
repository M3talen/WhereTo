using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WhereTo.Helpers;
using WhereTo.Models;
using Xamarin.Forms;

namespace WhereTo.ViewModels
{
    class AllItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Event> Events { get; set; }
        public Command LoadItemsCommand { get; set; }

        public AllItemsViewModel()
        {
            Title = "Browse";
            Events = new ObservableRangeCollection<Event>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {

            try
            {
                Events.Clear();
                var lat = Application.Current.Properties["lat"] as double?;
                var longi = Application.Current.Properties["long"] as double?;
                var radius = Application.Current.Properties["radius"] as double?;
                var items = await EventDataStore.GetItemsAsync(longi ?? 0, lat ?? 0, radius*25 ?? 0);
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
        }
    }
}
