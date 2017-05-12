using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TF.Views;
using Xamarin.Auth;
using System;
using TF.Renderers;
using UIKit;
using TF;

[assembly: ExportRenderer(typeof(BaseContentPage), typeof(LoginPageRenderer))]
namespace TF.Renderers
{
	public class LoginPageRenderer : PageRenderer
	{
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			var auth = new OAuth2Authenticator(
				clientId: "303166030106063", // your OAuth2 client id
				scope: "", // the scopes for the particular API you're accessing, delimited by "+" symbols
				authorizeUrl: new Uri("https://www.facebook.com/dialog/oauth"), // the auth URL for the service
				redirectUrl: new Uri("http://windows:8080/login_success.html")); // the redirect URL for the service

			auth.Completed += (sender, eventArgs) =>
			{
				// We presented the UI, so it's up to us to dimiss it on iOS.
				App.SuccessfulLoginAction.Invoke();

				if (eventArgs.IsAuthenticated)
				{
					// Use eventArgs.Account to do wonderful things
					App.SaveToken(eventArgs.Account.Properties["access_token"]);
				}
				else
				{
					// The user cancelled
				}
			};

			UIViewController vc = (UIKit.UIViewController)auth.GetUI();

			ViewController.AddChildViewController(vc);
			ViewController.View.Add(vc.View);

			// add out custom cancel button, to be able to navigate back
			vc.ChildViewControllers[0].NavigationItem.LeftBarButtonItem = new UIBarButtonItem(
				UIBarButtonSystemItem.Cancel, async (o, eargs) => await App.Current.MainPage.Navigation.PopModalAsync()
			);
		}
	}
}