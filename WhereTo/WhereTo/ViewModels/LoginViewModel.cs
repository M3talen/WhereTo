using System.Windows.Input;
using Xamarin.Forms;

namespace WhereTo.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {

        public LoginViewModel()
        {
            Title = "About";
            
            FacebookLoginCommand = new Command(FacebookLogin);

            GoogleLoginCommand = new Command(GoogleLogin);
        }

        private void GoogleLogin()
        {
            
        }

        private void FacebookLogin()
        {
            throw new System.NotImplementedException();
        }

        public ICommand FacebookLoginCommand { get; }

        public ICommand GoogleLoginCommand { get; }
    }
}