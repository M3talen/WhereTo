using System;
using Android.Widget;
using WhereTo.Models;

using Xamarin.Forms;

namespace WhereTo.Views
{
    public partial class NewItemPage : ContentPage
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
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine($"{_event.StartDate} - {_event.StartTime}");
            Toast.MakeText(Forms.Context, $"Added {_event}", ToastLength.Long).Show();
            MessagingCenter.Send(this, "AddItem", _event);
            await Navigation.PopToRootAsync();
        }
    }
}