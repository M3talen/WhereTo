using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Android.Widget;
using NControl.Controls;
using NControl.Controls.Droid;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using WhereTo.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Point = NGraphics.Point;

namespace WhereTo.Views
{
    public partial class NewItemPage : PopupPage
    {
        public Event _event { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            _event = new Event()
            {
                EventName = "Event Name",
                Description = "",
                StartDate = DateTime.Now,
                StartTime = DateTime.Now.TimeOfDay,
                EndDate = DateTime.Now,
                EndTime = DateTime.Now.TimeOfDay
            };
            AddItemCommand = new Command(AddItem);
            BindingContext = this;

        }


        public Command AddItemCommand { get; set; }
        // Method for animation child in PopupPage
        // Invoced after custom animation end
        protected new virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin
        protected new virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

        private async void AddItem()
        {
            Console.WriteLine($"{_event.StartDate} - {_event.StartTime}");
            Toast.MakeText(Forms.Context, $"Added {_event}", ToastLength.Long).Show();
            MessagingCenter.Send(this, "AddItem", _event);
            await Navigation.PopPopupAsync();
        }
       
        private void StartDate_OnFocused(object sender, FocusEventArgs e)
        {
            (sender as FloatingLabelControl)?.Unfocus();
            UserDialogs.Instance.DatePrompt(new DatePromptConfig { OnAction = (result) => SetStartDate(result), IsCancellable = true });
        }

        private void StartTime_OnFocused(object sender, FocusEventArgs e)
        {
            (sender as FloatingLabelControl)?.Unfocus();
            UserDialogs.Instance.TimePrompt(new TimePromptConfig {OnAction = (result) => SetStartTime(result), Use24HourClock = true, IsCancellable = true});
        }

        private void EndDate_OnFocused(object sender, FocusEventArgs e)
        {
            (sender as FloatingLabelControl)?.Unfocus();
            UserDialogs.Instance.DatePrompt(new DatePromptConfig { OnAction = (result) => SetEndDate(result), IsCancellable = true });
        }

        private void EndTime_OnFocused(object sender, FocusEventArgs e)
        {
            (sender as FloatingLabelControl)?.Unfocus();
            UserDialogs.Instance.TimePrompt(new TimePromptConfig { OnAction = (result) => SetEndTime(result), Use24HourClock = true, IsCancellable = true });
        }

        private void SetStartDate(DatePromptResult result)
        {
            if (!result.Ok) return;
            _event.StartDate = result.SelectedDate;
            StartDatePicker.Text = _event.StartDate.ToString("d")
        }
        private void SetEndDate(DatePromptResult result)
        {
            if (!result.Ok) return;
            _event.EndDate = result.SelectedDate;
            EndDatePicker.Text = _event.EndDate.ToString("d");
        }
        private void SetStartTime(TimePromptResult result)
        {
            if (!result.Ok) return;
            _event.StartTime = result.SelectedTime;
            StartTimePicker.Text = _event.StartTime.ToString();
        }
        private void SetEndTime(TimePromptResult result)
        {
            if (!result.Ok) return;
            _event.EndTime = result.SelectedTime;
            EndTimePicker.Text = _event.EndTime.ToString();
        }

        private void LocationPicker_OnFocused(object sender, FocusEventArgs e)
        {
            (sender as FloatingLabelControl)?.Unfocus();

            
        }
    }
}