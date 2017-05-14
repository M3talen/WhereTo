using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using Plugin.LocalNotifications;
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
		    JoinEvent = new Command(() =>
		    {
		        if (JoinButton.Text.Equals("Save"))
		        {
		            MessagingCenter.Send(this, "UpdateEvent",Item);
		        }
		        else
		        {
		            CrossLocalNotifications.Current.Show("Where To - Joined event", $"Joined event { Item.EventName } " +
		                                                                            $"at {Item.StartDate.ToShortDateString()} - {Item.StartTime}");

                    var date = Item.StartDate;
		            date.AddHours(Item.StartTime.Hours - 1);
		            date.AddMinutes(Item.StartTime.Minutes);
		            CrossLocalNotifications.Current.Show("Reminder", $"Pending event { Item.EventName } at {Item.StartDate.ToShortDateString()} - {Item.StartTime}", 101, date);
		        }
		    });
        }

        public Button JoinButton { get; set; }
	    public ICommand JoinEvent { get; set; }

        int quantity = 1;
		public int Quantity
		{
			get { return quantity; }
			set { SetProperty(ref quantity, value); }
		}
	}
}