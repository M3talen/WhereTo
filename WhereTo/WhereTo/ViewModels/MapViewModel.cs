using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WhereTo.Helpers;
using WhereTo.Models;
using WhereTo.Views;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace WhereTo.ViewModels
{
	public class MapViewModel : BaseViewModel
	{
	    public ObservableRangeCollection<Event> Events { get; set; }
	    public Command LoadItemsCommand { get; set; }

        public MapViewModel()
		{
		    Title = "Where To";
		    Events = new ObservableRangeCollection<Event>();
		    LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

		    MessagingCenter.Subscribe<NewItemPage, Event>(this, "AddItem", async (obj, item) =>
		    {
		        var _item = item as Event;
		        Events.Add(_item);
		        await EventDataStore.AddItemAsync(_item);
		    });


		    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
		    {
		        Device.StartTimer(TimeSpan.FromSeconds(10), () =>
		        {
		            LoadItemsCommand.Execute(null);
		            foreach (var tEvent in Events)
		            {
		                Position EventLocation = new Position(tEvent.Latitude, tEvent.Longitude);
		                var pin = new Pin() {Label = tEvent.EventName, Position = EventLocation};
		                GoogleMaps.Pins.Add(pin);
		            }
		            return true; // True = Repeat again, False = Stop the timer
                });
		        
		    });
        }

        public Map GoogleMaps { get; set; }


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
                var items = await EventDataStore.GetItemsAsync(longi ?? 0, lat ?? 0, radius ?? 0);
                Events.ReplaceRange(items.Distinct());
	            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
	            {
	                if (GoogleMaps != null)
	                {
                        GoogleMaps.Pins.Clear();
	                    foreach (var tEvent in Events)
	                    {
	                        Position EventLocation = new Position(tEvent.Latitude, tEvent.Longitude);
	                        var pin = new Pin() {Label = tEvent.EventName, Position = EventLocation};
	                        GoogleMaps.Pins.Add(pin);
	                    }
	                }
	            });
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
