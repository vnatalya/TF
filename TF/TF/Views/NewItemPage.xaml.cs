using System;
using System.Threading;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using TF.Models;

using Xamarin.Forms;

namespace TF.Views
{
    public partial class NewItemPage : ContentPage
    {
		TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }
        TriathlonTraining currentItem { get { return TriathlonViewModel.Instance.CurrentItem; } }

		double distance = 0;
		Position oldPosition;

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

		async void Start_Clicked(object sender, System.EventArgs e)
		{
			var geolocator = CrossGeolocator.Current;

			if (!geolocator.IsGeolocationAvailable)
			{
				DisplayAlert("Error", "Location is not awailable", "Ok");
				return;
			}

			Position result = null;

			try
			{
				geolocator.DesiredAccuracy = 50;

				if (!geolocator.IsListening)
					await geolocator.StartListeningAsync(5, 10);

				oldPosition = await geolocator.GetPositionAsync(50000, CancellationToken.None);
				geolocator.PositionChanged += Geolocator_PositionChanged;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Error : {0}", ex);
			}
		}

		void Geolocator_PositionChanged(object sender, PositionEventArgs e)
		{
			if (oldPosition != null)
				distance += GeoCodeCalc.CalcDistance(oldPosition.Latitude, oldPosition.Longitude, e.Position.Latitude, e.Position.Longitude);
			else
				oldPosition = e.Position;
		}

		private void Distance_TextChanged(object sender, TextChangedEventArgs e)
        {
        }


		public static class GeoCodeCalc
		{
			public const double EarthRadiusInMiles = 3956.0;
			public const double EarthRadiusInKilometers = 6367.0;

			public static double ToRadian(double val) { return val * (Math.PI / 180); }
			public static double DiffRadian(double val1, double val2) { return ToRadian(val2) - ToRadian(val1); }

			public static double CalcDistance(double lat1, double lng1, double lat2, double lng2)
			{
				return CalcDistance(lat1, lng1, lat2, lng2, GeoCodeCalcMeasurement.Miles);
			}

			public static double CalcDistance(double lat1, double lng1, double lat2, double lng2, GeoCodeCalcMeasurement m)
			{
				double radius = GeoCodeCalc.EarthRadiusInMiles;

				if (m == GeoCodeCalcMeasurement.Kilometers) { radius = GeoCodeCalc.EarthRadiusInKilometers; }
				return radius * 2 * Math.Asin(Math.Min(1, Math.Sqrt((Math.Pow(Math.Sin((DiffRadian(lat1, lat2)) / 2.0), 2.0) + Math.Cos(ToRadian(lat1)) * Math.Cos(ToRadian(lat2)) * Math.Pow(Math.Sin((DiffRadian(lng1, lng2)) / 2.0), 2.0)))));
			}
		}

		public enum GeoCodeCalcMeasurement : int
		{
			Miles = 0,
			Kilometers = 1
		}
    }
}