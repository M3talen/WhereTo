using System;
using System.Collections.Generic;
using System.Text;
using WhereTo.Views;
using Xamarin.Forms;

namespace WhereTo.Helpers
{
    class OAuthConfig
    {
        static NavigationPage _NavigationPage;
        public static UserDetails User;

        public static Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() =>
                {
                    _NavigationPage.Navigation.PushModalAsync(GetMainPage());
                });
            }
        }

        private static Page GetMainPage()
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
                        Title = "Browse",
                        Icon = Device.OnPlatform<string>("tab_feed.png", null, null)
                    }
                }
            };
        }
    }
}
