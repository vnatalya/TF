using System;
using Xamarin.Forms;

namespace TF
{
	public class TFDatePicker : ContentPage
	{
		public EventHandler<DateChangedEventArgs> OnDateSelected;
		public TFDatePicker(DateTime date)
		{
			var datePicker = new DatePicker
			{
				Date = date,
				MaximumDate = DateTime.Today,
			};

			datePicker.DateSelected += (sender, e) =>
			{
				if (OnDateSelected != null)
					OnDateSelected.Invoke(sender, e);
			};

			Content = datePicker;
		}
	}
}
