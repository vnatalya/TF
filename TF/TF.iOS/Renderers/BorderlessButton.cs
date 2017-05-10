using Xamarin.Forms;
using TF.Renderers;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessButton), typeof(BorderlessButtonRenderer))]
namespace TF.Renderers
{
	public class BorderlessButtonRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

		}
    }
}