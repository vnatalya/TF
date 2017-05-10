using System;

using TF.Models;

using Xamarin.Forms;

namespace TF.Views
{
    public partial class NewItemPage : ContentPage
    {
		TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }
        TriathlonTraining currentItem { get { return TriathlonViewModel.Instance.CurrentItem; } }

        public NewItemPage()
        {
            InitializeComponent();
            
			BindingContext = viewModel;
			DateButton.MaximumDate = DateTime.Today;
        }

        private void TimeButton_Click(object sender, EventArgs e)
        {
            TimePicker timePickerDialog = new TimePicker();
            timePickerDialog.Time = currentItem.Time;
        }

		private void TimeSelectedEvent(object sender, EventArgs e)
        {
            //currentItem.Time = new TimeSpan(e.HourOfDay, e.Minute, 0);
            //timeButton.Text = currentItem.Time.ToString();
        }

		async void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
		{
			currentItem.Date = e.NewDate;
		}

        private void TypeButton_Click(object sender, EventArgs e)
        {

        }

        private void Save_Clicked(object sender, EventArgs e)
        {
          //  currentItem.Distance = int.Parse(kilometersEditText.Text) * 1000 + int.Parse(metersEditText.Text);
            var result = viewModel.SaveCurrentItem();
        }

        private void Distance_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}