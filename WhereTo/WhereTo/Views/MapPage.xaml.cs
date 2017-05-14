
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Android.Gms.Maps;
using Android.Widget;
using NControl.Controls;
using Plugin.Geolocator;
using Rg.Plugins.Popup.Extensions;
using WhereTo.ViewModels;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using CameraUpdateFactory = Xamarin.Forms.GoogleMaps.CameraUpdateFactory;

namespace WhereTo.Views
{
    public partial class MapPage : ContentPage
    {
        MapViewModel _viewModel;
        public static bool first;

        public MapPage()
        {
            InitializeComponent();
            first = true;
            BindingContext = _viewModel = new MapViewModel();

            _viewModel.GoogleMaps = GoogleMaps;
            Task.Run(async () =>
            {
                await GetUserPositionAsync();
            });
            
            GoogleMaps.Padding = new Thickness(0, 0, 0, 0);


            /*
                        locator.PositionChanged += (sender, e) => {
                            var position = e.Position;

                            latitudeLabel.Text = position.Latitude;
                            longitudeLabel.Text = position.Longitude;
                        }*/
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!first)
            {


                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    _viewModel.LoadItemsCommand.Execute(null);
                    foreach (var tEvent in _viewModel.Events)
                    {
                        Position EventLocation = new Position(tEvent.Latitude, tEvent.Longitude);
                        var pin = new Pin() { Label = tEvent.EventName, Position = EventLocation };
                        GoogleMaps.Pins.Add(pin);
                    }
                });
            }
            first = false;
           
        }
        
        private async Task GetUserPositionAsync()
        { 
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                
                var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                if (position == null)
                    return;
                
                var center = new Position(position.Latitude, position.Longitude);


                var circle = new Circle
                {
                    Center = center,
                    Radius = Distance.FromMeters(1250f),
                    StrokeColor = (Color) Application.Current.Resources["PrimaryDark"],
                    StrokeWidth = 3f,
                    FillColor = Color.FromRgba(255, 152, 0, 32)
                };
                Application.Current.Properties["radius"] = 1.25;
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    ActivityIndicator.IsRunning = false;
                    ActivityIndicator.IsVisible = false;
                    GoogleMaps.Circles.Add(circle);
                    GoogleMaps.MoveToRegion(MapSpan.FromCenterAndRadius(center, Distance.FromMeters(1500)));
                    Application.Current.Properties["lat"] = position.Latitude;
                    Application.Current.Properties["long"] = position.Longitude;

                });

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    _viewModel.LoadItemsCommand.Execute(null);
                    foreach (var tEvent in _viewModel.Events)
                    {
                        Position EventLocation = new Position(tEvent.Latitude, tEvent.Longitude);
                        var pin = new Pin() { Label = tEvent.EventName, Position = EventLocation };
                        GoogleMaps.Pins.Add(pin);
                    }
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new NewItemPage());
        }
    }
}
