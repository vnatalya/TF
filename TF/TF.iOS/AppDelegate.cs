
using Foundation;
using UIKit;

namespace TF.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new App());

			App.DeviceWidth = (float) UIScreen.MainScreen.Bounds.Width;
			App.DeviceHeight = (float) UIScreen.MainScreen.Bounds.Height;

			return base.FinishedLaunching(app, options);
		}
	}
}
