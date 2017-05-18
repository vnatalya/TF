using System;
using System.Collections.Generic;

using Xamarin.Forms;
using TF.Views;

namespace TF
{
	public partial class StudentsOverviewPage : ContentPage
	{
		TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }

		public StudentsOverviewPage ()
		{
			InitializeComponent ();
			BindingContext = viewModel;
		}

        async void OnItemSelected (object sender, SelectedItemChangedEventArgs args)
		{
			await Navigation.PushAsync (new ItemsPage ());

			// Manually deselect item
			StudentsListView.SelectedItem = null;
		}

		private void DeleteButton_Clicked (object sender, System.EventArgs e)
		{
			DisplayAlert (string.Empty, StringService.Instance.DeleteAlert, "Ok", "Cancel");
		}
	}
}
