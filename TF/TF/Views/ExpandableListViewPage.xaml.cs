using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TF
{
	public partial class ExpandableListViewPage : ContentPage
	{
		TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }
		
		public ExpandableListViewPage(bool showTriathlon)
		{
			InitializeComponent();
			BindingContext = viewModel;
		}

		private async void Button_Click(object sender, System.EventArgs e)
		{
			
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			
		}
	}
}
