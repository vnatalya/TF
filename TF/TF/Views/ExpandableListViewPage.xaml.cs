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
			CyclingListView.IsVisible = false;
			RunningListView.IsVisible = false;
			SwimmingListView.IsVisible = false;

			if (sender == CyclingButton)
				CyclingListView.IsVisible = true;

			if (sender == RunningButton)
				RunningListView.IsVisible = true;

			if (sender == SwimmingButton)
				SwimmingListView.IsVisible = true;

			if (sender == TriathlonButton)
			{
				viewModel.CurrentType = TriathlonTraining.TriathlonType.Triathlon;
				Navigation.PopModalAsync();
			}
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			if (sender == SwimmingListView)
				viewModel.CurrentType = (TriathlonTraining.TriathlonType)((int)TriathlonTraining.TriathlonType.Swimming + viewModel.SwimmingList.IndexOf((NamedItem)args.SelectedItem));

			if (sender == RunningListView)
				viewModel.CurrentType = (TriathlonTraining.TriathlonType)((int)TriathlonTraining.TriathlonType.Running + viewModel.RunningList.IndexOf((NamedItem)args.SelectedItem));

			if (sender == CyclingListView)
				viewModel.CurrentType = (TriathlonTraining.TriathlonType)((int)TriathlonTraining.TriathlonType.Cycling + viewModel.CyclingList.IndexOf((NamedItem)args.SelectedItem));

			Navigation.PopModalAsync();
		}
	}
}
