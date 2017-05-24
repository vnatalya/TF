using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TF
{
	public partial class FeedbackOverviewPage : ContentPage
	{
		public FeedbackOverviewPage ()
		{
			InitializeComponent ();
			BindingContext = UserViewModel.Instance;
		}
	}
}
