using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TF
{
	public partial class GroupsListPage : ContentPage
	{
		UserViewModel viewModel { get { return UserViewModel.Instance; } }

		public GroupsListPage ()
		{
			InitializeComponent ();
			BindingContext = viewModel;

			NavigationPage.SetBackButtonTitle (this, StringService.Instance.Back);
		}

		async void OnItemSelected (object sender, SelectedItemChangedEventArgs args)
		{
			await Navigation.PushAsync (new StudentsOverviewPage ());
		}

		private async void DeleteButton_Clicked (object sender, System.EventArgs e)
		{
			var item = (sender as Button).BindingContext as Group;
			var res = await viewModel.DeleteGroup (item.ID);
			if (res.Status)
				GroupsListView.ItemsSource = viewModel.GroupsList;
			else
				DisplayAlert (res.Title, res.ErrorMessage, StringService.Instance.Ok);
		}

		private async void AddButton_Clicked (object sender, System.EventArgs e)
		{
			var res = await viewModel.AddNewGroup (NewGroupName.Text);
			if (res.Status)
				GroupsListView.ItemsSource = viewModel.GroupsList;
			else
				DisplayAlert (res.Title, res.ErrorMessage, StringService.Instance.Ok);
		}
	}
}
