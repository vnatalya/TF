using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.OS;
using TF.Renderers;

[assembly: ExportRenderer(typeof(ShortListView), typeof(ShortListViewRenderer))]
namespace TF.Renderers
{
	public class ShortListViewRenderer : ListViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
		{
			base.OnElementChanged(e);
			//if (e.OldElement == null)
			//{
			//    if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Lollipop)
			//    {
			//        Control.StateListAnimator = null;
			//    }
			//}
		}
	}
}