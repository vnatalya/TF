using System;
using System.Collections.Generic;

using Xamarin.Forms;
using TF.Views;

namespace TF
{
	public partial class StudentsOverviewPage : ContentPage
	{
		UserViewModel viewModel { get { return UserViewModel.Instance; } }

		public StudentsOverviewPage ()
		{
			InitializeComponent ();
			BindingContext = viewModel;

			NavigationPage.SetBackButtonTitle (this, StringService.Instance.Back);
		}

        async void OnItemSelected (object sender, SelectedItemChangedEventArgs args)
		{
			var item = StudentsListView.BindingContext as User;
			var res = await TriathlonViewModel.Instance.GetTrainings ();
			if (res.Status)
				await Navigation.PushAsync (new ItemsPage ());
			else
				DisplayAlert (res.Title, res.ErrorMessage, StringService.Instance.Ok);
		}

		private async void DeleteButton_Clicked (object sender, System.EventArgs e)
		{
			var item = (sender as Button).BindingContext as User;
			var res = await viewModel.DeleteStudentFromGroup (item.ID);
		}

		private void AddFeedbackButton_Clicked (object sender, System.EventArgs e)
		{
			Navigation.PushAsync (new AddFeedbackPage());
		}
	}
}
