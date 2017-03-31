using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace WhereTo.ViewModels
{
	public class MapViewModel : BaseViewModel
	{
		public MapViewModel()
		{
			Title = "Event Map";

			OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
		}

		/// <summary>
		/// Command to open browser to xamarin.com
		/// </summary>
		public ICommand OpenWebCommand { get; }
	}
}
