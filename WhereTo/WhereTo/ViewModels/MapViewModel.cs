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
        }

	    async Task ExecuteLoadItemsCommand()
	    {
            /*
	        if (IsBusy)
	            return;

	        IsBusy = true;*/

	        try
	        {
	            Events.Clear();
                var lat = Application.Current.Properties["lat"] as double?;
                var longi = Application.Current.Properties["long"] as double?;
                var radius = Application.Current.Properties["radius"] as double?;
                var items = await EventDataStore.GetItemsAsync(longi ?? 0, lat ?? 0, radius ?? 0);
                Events.ReplaceRange(items.Distinct());
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
