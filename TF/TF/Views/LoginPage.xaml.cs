using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
//using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
          
                // activity.StartActivity(auth.GetUI(activity));

        }

        private void Login_OnClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform("tab_feed.png",null,null),
						BarBackgroundColor = Color.Green
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png",null,null),
						BarBackgroundColor = Color.Green
                    },
                }
            };
        }

        private void GoogleLogin_OnClicked(object sender, EventArgs e)
        {

        }

        private void CreateAccount_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
        }

        private void FacebookLogin_OnClicked(object sender, EventArgs e)
        {
			var auth = new OAuth2Authenticator(
				  clientId: "303166030106063", // your OAuth2 client id
				  scope: "", // the scopes for the particular API you're accessing, delimited by "+" symbols
				  authorizeUrl: new Uri("https://www.facebook.com/dialog/oauth"), // the auth URL for the service
				  redirectUrl: new Uri("http://windows:8080/login_success.html")); // the redirect URL for the service

            auth.Completed += (s, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    App.SuccessfulLoginAction.Invoke();
                    // Use eventArgs.Account to do wonderful things
                    App.SaveToken(eventArgs.Account.Properties["access_token"]);
                }
                else
                {
                    // The user cancelled
                }
            };
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
    

