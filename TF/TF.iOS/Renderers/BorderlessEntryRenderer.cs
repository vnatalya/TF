using Xamarin.Forms;
using TF.Renderers;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer (typeof (BorderlessEntry), typeof (BorderlessEntryRenderer))]
namespace TF.Renderers
{
	public class BorderlessEntryRenderer : Xamarin.Forms.Platform.iOS.EntryRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

			if (this.Control == null) 
				return;
			
			this.Control.BorderStyle = UITextBorderStyle.None;
		}
	}
}