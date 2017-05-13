using Xamarin.Forms;
using TF.Renderers;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using TF;
using CoreGraphics;

[assembly: ExportRenderer(typeof(ShortListView), typeof(TF.Renderers.ListViewRenderer))]
namespace TF.Renderers
{
	public class ListViewRenderer : Xamarin.Forms.Platform.iOS.ListViewRenderer
	{
		public ListViewRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);

			if (this.Control == null) return;

			this.Control.TableFooterView = new UIView(new CGRect(0,0,0,0));
		}
	}
}
