using WhereTo.Models;

namespace WhereTo.ViewModels
{
	public class ItemDetailViewModel : BaseViewModel
	{
		public Event Item { get; set; }
		public ItemDetailViewModel(Event item = null)
		{
			Title = item.EventName;
			Item = item;
		}

		int quantity = 1;
		public int Quantity
		{
			get { return quantity; }
			set { SetProperty(ref quantity, value); }
		}
	}
}