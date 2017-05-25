using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TF
{
	public partial class AddFeedbackPage : ContentPage
	{
		UserViewModel viewModel { get { return UserViewModel.Instance; } }
		
		public AddFeedbackPage ()
		{
			InitializeComponent ();

			var toolBar = new ToolbarItem { Name = StringService.Instance.Save };
			toolBar.Clicked += Save_Clicked;
			ToolbarItems.Add (toolBar);

			NavigationPage.SetBackButtonTitle (this, StringService.Instance.Back);
		}

		void Handle_Focused (object sender, FocusEventArgs e)
		{
			var editor = sender as Editor;

			if (!string.IsNullOrEmpty (editor.Text) && editor.Text.Equals (viewModel.StringEnterFeedback))
					editor.Text = string.Empty;
		}

		async void Save_Clicked (object sender, EventArgs e)
		{
			var res = await viewModel.AddFeedback (FeedbackEditor.Text);
			if (res.Status)
				await Navigation.PopAsync ();
			else
				DisplayAlert (res.Title, res.ErrorMessage, StringService.Instance.Ok);
		}
	}
}
