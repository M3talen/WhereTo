using Android.App;
using WhereTo.Droid.PageRenderers;
using WhereTo.Helpers;
using WhereTo.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRenderer))]
namespace WhereTo.Droid.PageRenderers
{
    public class LoginRenderer : PageRenderer
    {
        bool showLogin = true;
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            //Get and Assign ProviderName from ProviderLoginPage  
            var loginPage = Element as ProviderLoginPage;
            string providername = loginPage.ProviderName;
            var activity = this.Context as Activity;
            if (showLogin && OAuthConfig.User == null)
            {
                showLogin = false;
                //Create OauthProviderSetting class with Oauth Implementation .Refer Step 6  
                OAuthProviderSetting oauth = new OAuthProviderSetting();
                var auth = oauth.LoginWithProvider(providername);
                // After facebook,google and all identity provider login completed   
                auth.Completed += (sender, eventArgs) => {
                    if (eventArgs.IsAuthenticated)
                    {
                        OAuthConfig.User = new UserDetails
                        {
                            Token = eventArgs.Account.Properties["oauth_token"],
                            TokenSecret = eventArgs.Account.Properties["oauth_token_secret"],
                            TwitterId = eventArgs.Account.Properties["user_id"],
                            ScreenName = eventArgs.Account.Properties["screen_name"]
                        };
                        // Get and Save User Details   
                        OAuthConfig.SuccessfulLoginAction.Invoke();
                    }
                    else
                    {
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                    }
                };
                activity.StartActivity(auth.GetUI(activity));
            }
        }
    }
}