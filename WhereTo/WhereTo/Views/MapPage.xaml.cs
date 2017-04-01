
using System;
using System.Threading.Tasks;
using Android.Gms.Maps;
using Android.Widget;
using NControl.Controls;
using Plugin.Geolocator;
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

        public MapPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MapViewModel();

            FABAdd.ButtonIcon = FontAwesomeLabel.FAPlus;

            Task.Run(async () =>
            {
                await GetUserPositionAsync();
            });
            
            GoogleMaps.Padding = new Thickness(0, 0, 0, 0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Events.Count == 0)
                _viewModel.LoadItemsCommand.Execute(null);

            foreach (var tEvent in _viewModel.Events)
            {
                var pin = new Pin() {Label = tEvent.EventName, Position = tEvent.EventLocation};
                GoogleMaps.Pins.Add(pin);
            }
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
                
           
                var circle = new Circle();
                circle.Center = center;
                circle.Radius = Distance.FromMeters(1250f);
                circle.StrokeColor = (Color) Application.Current.Resources["PrimaryDark"];
                circle.StrokeWidth = 3f;
                circle.FillColor = Color.FromRgba(255, 152, 0, 32);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    GoogleMaps.Circles.Add(circle);
                    GoogleMaps.MoveToRegion(MapSpan.FromCenterAndRadius(center, Distance.FromMeters(1500)), false);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
