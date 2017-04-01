using System;
using System.Diagnostics;
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
		        await DataStore.AddItemAsync(_item);
		    });
        }

	    async Task ExecuteLoadItemsCommand()
	    {
	        if (IsBusy)
	            return;

	        IsBusy = true;

	        try
	        {
	            Events.Clear();
	            var items = await DataStore.GetItemsAsync(true);
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
