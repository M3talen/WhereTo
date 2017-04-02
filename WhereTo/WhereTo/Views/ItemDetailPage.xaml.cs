
using System.Threading.Tasks;
using Android.Support.Design.Widget;
using Rg.Plugins.Popup.Pages;
using WhereTo.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace WhereTo.Views
{
	public partial class ItemDetailPage : PopupPage
    {
		ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
		{
			InitializeComponent();
            
            BindingContext = this.viewModel = viewModel;

		    GoogleMapsPreview?.MoveToRegion(MapSpan.FromCenterAndRadius(viewModel.Item.EventLocation, Distance.FromMeters(1000)), false);
		    var pin = new Pin() { Label = viewModel.Item.EventName, Position = viewModel.Item.EventLocation };
		    GoogleMapsPreview?.Pins.Add(pin);

		}

        protected new virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }
        
        protected new virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

    }
}
