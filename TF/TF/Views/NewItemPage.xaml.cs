using System;
using System.Threading;
using System.Threading.Tasks;
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
        bool isTracking;
        IGeolocator geolocator;

        public NewItemPage()
        {
            InitializeComponent();
            
			BindingContext = viewModel;
			DatePicker.MaximumDate = DateTime.Today;

			if (viewModel.IsEditMode) {
				var toolBar = new ToolbarItem { Name = StringService.Instance.Save };
				toolBar.Clicked += Save_Clicked;

				ToolbarItems.Add (toolBar);
			}

			NavigationPage.SetBackButtonTitle (this, StringService.Instance.Back);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            StartButton.IsVisible = (int) viewModel.CurrentType % 10 != 0;
        }

        private void Time_TextChanged(object sender, TextChangedEventArgs e)
        {
			int newValue;
			if (!int.TryParse (e.NewTextValue, out newValue) || newValue > 60)
				(sender as Entry).Text = !string.IsNullOrEmpty (e.OldTextValue) ? e.OldTextValue : string.Empty;
				
        }

        private void TypeButton_Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TypePickerPage(false));
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {            
            currentItem.Distance = Double.Parse(DistanceEntry.Text);
            currentItem.Date = DatePicker.Date;
            currentItem.Time = new TimeSpan(!string.IsNullOrEmpty(HoursEntry.Text) ? int.Parse(HoursEntry.Text) : 0,
                !string.IsNullOrEmpty(MinutesEntry.Text) ? int.Parse(MinutesEntry.Text) : 0,
                !string.IsNullOrEmpty(SecondsEntry.Text) ? int.Parse(SecondsEntry.Text) : 0);

            var result = await viewModel.SaveCurrentItem();
            if (!result.Status)
            {
               // DisplayAlert(result.Title, result.ErrorMessage, StringService.Instance.Ok);
            }
            else
                Navigation.PopAsync();
        }

		async void Start_Clicked(object sender, System.EventArgs e)
		{
            if (isTracking)
            {
                (sender as Button).Text = StringService.Instance.Start;

                isTracking = false;

                if (geolocator == null)
                    return;

               await geolocator.StopListeningAsync();

            }
            else
            {
                (sender as Button).Text = StringService.Instance.Finish;
                isTracking = true;
                StartTimer();
				bool successStart = await StartTracking ();
            	if (!successStart)
                {
					(sender as Button).Text = StringService.Instance.Start;
                    isTracking = false;
				}
			}
		}

        void StartTimer()
        {
            int h = 0;
            int min = 0;
            int sec = 0;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
           {
               ++sec;
               if (sec == 60)
               {
                   ++min;
                   sec = 0;
               }
               if (min == 60)
               {
                   ++h;
                   min = 0;
               }

               HoursEntry.Text = h.ToString();
               MinutesEntry.Text = min.ToString();
               SecondsEntry.Text = sec.ToString();

               return isTracking;
           });
        }

		async Task<bool> StartTracking ()
		{
			geolocator = CrossGeolocator.Current;

			if (!geolocator.IsGeolocationAvailable) {
				DisplayAlert ("Error", "Location is not awailable", "Ok");
				return false;
			}

			Position result = null;

			try {
				geolocator.DesiredAccuracy = 50;

				if (!geolocator.IsListening)
					await geolocator.StartListeningAsync (5, 10);

				oldPosition = await geolocator.GetPositionAsync (50000, CancellationToken.None);
				geolocator.PositionChanged += Geolocator_PositionChanged;
			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine ("Error : {0}", ex);
				return false;
			}
			return true;
		}

		void Geolocator_PositionChanged(object sender, PositionEventArgs e)
		{
            if (oldPosition != null)
            {
                distance += GeoCodeCalc.CalcDistance(oldPosition.Latitude, oldPosition.Longitude, e.Position.Latitude, e.Position.Longitude);
                currentItem.Distance = distance;
                DistanceEntry.Text = currentItem.Distance.ToString();               
            }
            else
                oldPosition = e.Position;
		}

		private void Distance_TextChanged(object sender, TextChangedEventArgs e)
        {
            double distance;
            if (Double.TryParse(e.NewTextValue, out distance))
                currentItem.Distance = distance;
            else if (!string.IsNullOrEmpty(e.OldTextValue))
                DistanceEntry.Text = e.OldTextValue;
        }

		public static class GeoCodeCalc
		{
			public const double EarthRadiusInKilometers = 6367.0;

			public static double ToRadian(double val) { return val * (Math.PI / 180); }
			public static double DiffRadian(double val1, double val2) { return ToRadian(val2) - ToRadian(val1); }
            
			public static double CalcDistance(double lat1, double lng1, double lat2, double lng2)
			{
				double radius = GeoCodeCalc.EarthRadiusInKilometers; 
				return radius * 2 * Math.Asin(Math.Min(1, Math.Sqrt((Math.Pow(Math.Sin((DiffRadian(lat1, lat2)) / 2.0), 2.0) + Math.Cos(ToRadian(lat1)) * Math.Cos(ToRadian(lat2)) * Math.Pow(Math.Sin((DiffRadian(lng1, lng2)) / 2.0), 2.0)))));
			}
		}
    }
}