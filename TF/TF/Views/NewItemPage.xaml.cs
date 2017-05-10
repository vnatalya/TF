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
        }

        private void DateButton_Click(object sender, EventArgs e)
        {
            DatePicker datePicker = new DatePicker();
            datePicker.Date = currentItem.Date;
            datePicker.MaximumDate = DateTime.Today;
            datePicker.DateSelected += DateSelectedEvent;
        }

        private void TimeButton_Click(object sender, EventArgs e)
        {
            TimePicker timePickerDialog = new TimePicker();
            timePickerDialog.Time = currentItem.Time;
        }

        private void TimeSelectedEvent(int h, int min, int sec, int milisec)
        {
            //currentItem.Time = new TimeSpan(e.HourOfDay, e.Minute, 0);
            //timeButton.Text = currentItem.Time.ToString();
        }

        private void DateSelectedEvent(object sender, EventArgs e)
        {
            //currentItem.Date = e.Date;
            //timeButton.Text = currentItem.Date.ToString();
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