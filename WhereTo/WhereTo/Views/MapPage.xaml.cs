
using System;
using System.Threading.Tasks;
using Android.Gms.Maps;
using Android.Widget;
using Plugin.Geolocator;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using CameraUpdateFactory = Xamarin.Forms.GoogleMaps.CameraUpdateFactory;

namespace WhereTo.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

            BindingContext = this;

            var googleMapsOptions = new GoogleMapOptions();
            googleMapsOptions.InvokeLiteMode(true);
            googleMapsOptions.InvokeAmbientEnabled(true);

            Task.Run(async () =>
            {
                await GetUserPositionAsync();
            });
            

            var pinMelbourne = new Pin() { Label = "FER", Position = new Position(45.7976435, 15.9816766) };
            var pinNewyork = new Pin() { Label = "Piva", Position = new Position(45.7913905, 15.9568096) };
            var pinLisboa = new Pin() { Label = "Tolkien's House", Position = new Position(45.8162501, 15.9709311) };
            GoogleMaps.Pins.Add(pinMelbourne);
            GoogleMaps.Pins.Add(pinNewyork);
            GoogleMaps.Pins.Add(pinLisboa);

            GoogleMaps.Padding = new Thickness(0, 0, 0, 0);
        }


        private async Task GetUserPositionAsync()
        {
            try
            {
                Console.WriteLine("Getting location");
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                Console.WriteLine("Getting location 2");
                var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                if (position == null)
                {
                    Console.WriteLine("Getting location 3 - FAILED");
                    return;
                }

                Console.WriteLine("Getting location 4");
                Console.WriteLine("Position Status: {0}", position.Timestamp);
                Console.WriteLine("Position Latitude: {0}", position.Latitude);
                Console.WriteLine("Position Longitude: {0}", position.Longitude);
               

                Console.WriteLine("Getting location 5");
                var center = new Position(position.Latitude, position.Longitude);

                Console.WriteLine("Getting location 6");
                var circle = new Circle();
                circle.Center = center;
                circle.Radius = Distance.FromMeters(2000f);
                circle.StrokeColor = Color.Orange;
                circle.StrokeWidth = 3f;
                circle.FillColor = Color.FromRgba(0, 0, 255, 32);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    GoogleMaps.Circles.Add(circle);
                    GoogleMaps.MoveToRegion(MapSpan.FromCenterAndRadius(center, Distance.FromMeters(6000)), false);
                });
                Console.WriteLine("Getting location 7");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
