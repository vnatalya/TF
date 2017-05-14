using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TF
{
	public partial class TypePickerPage : ContentPage
	{
		TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }
        bool newItemMode;

		public TypePickerPage(bool showTriathlon)
		{
			InitializeComponent();
			BindingContext = viewModel;

            this.newItemMode = !showTriathlon;
            if (newItemMode)
            {
                TriathlonButton.IsVisible = false;                
            }
		}

		private void Button_Click(object sender, System.EventArgs e)
		{
            if (newItemMode)
            {
                if (sender == CyclingButton)
                    viewModel.CurrentItem.Type = TriathlonTraining.TriathlonType.Cycling;

                if (sender == RunningButton)
                    viewModel.CurrentItem.Type = TriathlonTraining.TriathlonType.Running;

                if (sender == SwimmingButton)
                    viewModel.CurrentItem.Type = TriathlonTraining.TriathlonType.Swimming;
            }
            else
            {
                if (sender == CyclingButton)
                    viewModel.CurrentType = TriathlonTraining.TriathlonType.Cycling;

                if (sender == RunningButton)
                    viewModel.CurrentType = TriathlonTraining.TriathlonType.Running;

                if (sender == SwimmingButton)
                    viewModel.CurrentType = TriathlonTraining.TriathlonType.Swimming;

                if (sender == TriathlonButton)
                    viewModel.CurrentType = TriathlonTraining.TriathlonType.Triathlon;
            }

            Navigation.PopAsync();
		}

        private void Type_Selected(object sender, System.EventArgs e)
        {
            CyclingListView.IsVisible = false;
            RunningListView.IsVisible = false;
            SwimmingListView.IsVisible = false;


            CyclingType.Image = "right_arrow";
            SwimmingType.Image = "right_arrow";
            RunningType.Image = "right_arrow";

            (sender as Button).Image = "down_button";

            if (sender == CyclingType)
                CyclingListView.IsVisible = true;

            if (sender == RunningType)
                RunningListView.IsVisible = true;

            if (sender == SwimmingType)
                SwimmingListView.IsVisible = true;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			if (sender == SwimmingListView)
				viewModel.CurrentType = (TriathlonTraining.TriathlonType)((int)TriathlonTraining.TriathlonType.Swimming + viewModel.SwimmingList.IndexOf((NamedItem)args.SelectedItem));

			if (sender == RunningListView)
				viewModel.CurrentType = (TriathlonTraining.TriathlonType)((int)TriathlonTraining.TriathlonType.Running + viewModel.RunningList.IndexOf((NamedItem)args.SelectedItem));

			if (sender == CyclingListView)
				viewModel.CurrentType = (TriathlonTraining.TriathlonType)((int)TriathlonTraining.TriathlonType.Cycling + viewModel.CyclingList.IndexOf((NamedItem)args.SelectedItem));

			Navigation.PopAsync();
		}
	}
}
