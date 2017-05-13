using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.OS;
using TF.Renderers;

[assembly: ExportRenderer(typeof(BorderlessButton), typeof(BorderlessButtonRenderer))]
namespace TF.Renderers
{
    public class BorderlessButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
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