using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TF
{
	public partial class FeedbackOverviewPage : ContentPage
	{
		UserViewModel viewModel { get { return UserViewModel.Instance; } }

		public FeedbackOverviewPage ()
		{
			InitializeComponent ();
			BindingContext = viewModel;
		}

		private async void DeleteButton_Clicked (object sender, System.EventArgs e)
		{
			var item = (sender as Button).BindingContext as Feedback;
			var res = await viewModel.DeleteFeedback (item.Id);
		}
	}
}
