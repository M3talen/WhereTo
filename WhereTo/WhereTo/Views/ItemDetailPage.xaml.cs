
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Android.Support.Design.Widget;
using NControl.Controls;
using Plugin.LocalNotifications;
using Rg.Plugins.Popup.Pages;
using WhereTo.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xfx;
using Android;
using Resource = Android.Resource;

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
		    viewModel.JoinButton = ButtonJoin;
            Position EventLocation = new Position(viewModel.Item.Latitude, viewModel.Item.Longitude);
		    GoogleMapsPreview?.MoveToRegion(MapSpan.FromCenterAndRadius(EventLocation, Distance.FromMeters(1000)), false);
		    var pin = new Pin() { Label = viewModel.Item.EventName, Position = EventLocation };
		    GoogleMapsPreview?.Pins.Add(pin);

		    var id = Application.Current.Properties["user_id"] as int?;
		    if (viewModel.Item.UserId.Equals(id.Value.ToString()))
		    {
		        EntryName.IsEnabled = true;
		        EntryDesc.IsEnabled = true;
		        EntryCath.IsEnabled = true;
		        StartDatePicker.IsEnabled = true;
		        StartTimePicker.IsEnabled = true;
		        EndDatePicker.IsEnabled = true;
		        EndTimePicker.IsEnabled = true;
		        ButtonJoin.Text = "Save";
		    }
		    else
		    {
		        
		            EntryName.IsEnabled = false;
		            EntryDesc.IsEnabled = false;
		            EntryCath.IsEnabled = false;
		            StartDatePicker.IsEnabled = false;
		            StartTimePicker.IsEnabled = false;
		            EndDatePicker.IsEnabled = false;
		            EndTimePicker.IsEnabled = false;
		        ButtonJoin.IsEnabled = true;
		            ButtonJoin.Text = "Join";
		        
            }

		   

        }

        protected new virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }
        
        protected new virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

        private void VisualElement_OnFocused(object sender, FocusEventArgs e)
        {
            (sender as FloatingLabelControl)?.Unfocus();
        }
    }
}
