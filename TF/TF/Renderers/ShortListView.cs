using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
namespace TF.Renderers
{
	public class ShortListView : ListView
	{
		public ShortListView() : base()
		{
		}

		protected override void OnParentSet()
		{
			base.OnParentSet();
			this.RowHeight = 47;
			if (ItemsSource != null)
				HeightRequest = ItemsSource.OfType<object>().Count() * 47;
		}
	}
}