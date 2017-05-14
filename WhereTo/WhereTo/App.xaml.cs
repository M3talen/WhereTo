using WhereTo.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WhereTo
{
    public partial class App : Application
    {

        public App()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            //TODO check auth status
            SetLoginPage();

        }
        public static void SetLoginPage()
        {
            if (Application.Current.Properties.ContainsKey("user_logged"))
            {
                Current.MainPage = GetMainPage();
            }
            else
            {
                Application.Current.Properties["user_id"] = 0;
                Current.MainPage = new NavigationPage(new LoginPage());
            }
        }

        public static Page GetMainPage()
        {
            return new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new MapPage())
                    {
                        Title = "Event Map",
                        Icon = Device.OnPlatform<string>("tab_about.png", null, null)
                    },new NavigationPage(new ItemsPage())
                    {
                        Title = "Nearby",
                        Icon = Device.OnPlatform<string>("tab_feed.png", null, null)
                    },new NavigationPage(new AllItemsPage())
                    {
                    Title = "Browse",
                    Icon = Device.OnPlatform<string>("tab_feed.png", null, null)
                    }
                }
            };
        }

    }
}
