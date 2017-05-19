using System;
using Xamarin.Forms;

namespace TF.Renderers
{
	public class BorderlessEntry : Entry
	{
		public BorderlessEntry () : base ()
		{
		}

		protected override void OnParentSet ()
		{
			base.OnParentSet ();
			this.BackgroundColor = Color.White;
			this.TextColor = Color.Green;
		}
	}
}
