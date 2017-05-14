
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


        public ICommand JoinEvent { get; }

        public ItemDetailPage(ItemDetailViewModel viewModel)
		{
			InitializeComponent();
            
            BindingContext = this.viewModel = viewModel;
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

		    JoinEvent= new Command(() =>
		    {
		        if (ButtonJoin.Text.Equals("Save"))
		        {
		            MessagingCenter.Send(this, "UpdateEvent", viewModel.Item);
		        }
		        else
		        {
		            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.IcDialogAlert;
                    CrossLocalNotifications.Current.Show("Where To - Joined event", $"Joined event { viewModel.Item.EventName } at {viewModel.Item.StartDate.ToShortDateString()} - {viewModel.Item.StartTime}");
		            var date = viewModel.Item.StartDate;
		            date.AddHours(viewModel.Item.StartTime.Hours - 1);
		            date.AddMinutes(viewModel.Item.StartTime.Minutes);
                    CrossLocalNotifications.Current.Show("Reminder", $"Pending event { viewModel.Item.EventName } at {viewModel.Item.StartDate.ToShortDateString()} - {viewModel.Item.StartTime}", 101, date);
                }
		        UserDialogs.Instance.Alert("Server request - null");
		    });

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
