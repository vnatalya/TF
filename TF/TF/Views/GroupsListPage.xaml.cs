using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TF
{
	public partial class GroupsListPage : ContentPage
	{
		TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }

		public GroupsListPage ()
		{
			InitializeComponent ();
		}

		async void OnItemSelected (object sender, SelectedItemChangedEventArgs args)
		{
			await Navigation.PushAsync (new StudentsOverviewPage ());

			// Manually deselect item
			GroupsListView.SelectedItem = null;
		}


		private void DeleteButton_Clicked (object sender, System.EventArgs e)
		{
			DisplayAlert (string.Empty, StringService.Instance.DeleteAlert, "Ok", "Cancel");
		}

		private void AddButton_Clicked (object sender, System.EventArgs e)
		{
			DisplayAlert (string.Empty, StringService.Instance.DeleteAlert, "Ok", "Cancel");
		}
	}
}
