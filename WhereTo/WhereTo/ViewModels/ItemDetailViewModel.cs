using System.Linq;
using WhereTo.Helpers;
using WhereTo.Models;
using WhereTo.Views;
using Xamarin.Forms;

namespace WhereTo.ViewModels
{
	public class ItemDetailViewModel : BaseViewModel
	{
	    public ObservableRangeCollection<Event> Events { get; set; }
        public Event Item { get; set; }
		public ItemDetailViewModel(Event item = null)
		{
			Title = item.EventName;
			Item = item;

		    MessagingCenter.Subscribe<ItemDetailPage, Event>(this, "UpdateEvent", async (obj, iteme) =>
		    {
		        await EventDataStore.UpdateItemAsync(iteme);
		    });

        }



		int quantity = 1;
		public int Quantity
		{
			get { return quantity; }
			set { SetProperty(ref quantity, value); }
		}
	}
}