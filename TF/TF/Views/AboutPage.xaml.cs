using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using TF.HelperViews;

namespace TF.Views
{
    public partial class AboutPage : ContentPage
    {
        TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }
        TriathlonTraining currentItem { get { return TriathlonViewModel.Instance.CurrentItem; } }
		SKCanvasView canvasView;

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
			ChartView.HeightRequest = App.DeviceWidth - 30;
			ChartView.WidthRequest = App.DeviceWidth * 2 - 60;
		}

        private void Button_Click(object sender, System.EventArgs e)
        {
			ContentPage filterPage =  sender == TypeButton ? new TypePickerPage(true) as ContentPage : new PeriodPickerPage() as ContentPage;
			Navigation.PushAsync(filterPage);
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			SetChart();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			RemoveChart();
		}

		void SetChart()
		{			
			canvasView = new SKCanvasView();
			canvasView.PaintSurface += OnCanvasViewPaintSurface;
			canvasView.WidthRequest = ChartView.Bounds.Width  - 40;
			canvasView.HeightRequest = ChartView.Bounds.Height - 60;
			ChartView.Children.Add(canvasView);
		}

		void RemoveChart()
		{
			canvasView.PaintSurface -= OnCanvasViewPaintSurface;
			ChartView.Children.Remove(canvasView);
		}


		void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
		{
			SKImageInfo info = args.Info;
			SKSurface surface = args.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			SKPaint textPaint = new SKPaint
			{
				Color = SKColors.Black,
				TextSize = 12,
				TextAlign = SKTextAlign.Center
			};

			var pointsCount = viewModel.Trainings.Count;

			var pictureWidth = info.Width;
			var pictureHeight = info.Height;

			var pointWidth = pictureWidth / pointsCount;
			double pointHeight = 1;

			double biggestValue = 0;

            #region draw time

            SKPaint timePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 2
            };

            List<SKPoint> timePoints = new List<SKPoint>();
			for (int i = 0; i < pointsCount; ++i)
			{
				biggestValue = viewModel.Trainings[i].Time.TotalMinutes > biggestValue ? viewModel.Trainings[i].Time.TotalMinutes : biggestValue;
				pointHeight = pictureHeight / biggestValue;
				canvas.DrawText(viewModel.Trainings[i].DisplayDate, i * pointWidth, pictureHeight - 10, textPaint);
				canvas.DrawText(viewModel.Trainings[i].Time.TotalMinutes.ToString(), 15, pictureHeight - 15 - (int)(viewModel.Trainings[i].Time.TotalMinutes * pointHeight), textPaint);
				
				timePoints.Add(new SKPoint(i * pointWidth, pictureHeight - 15 - (int)(viewModel.Trainings[i].Time.TotalMinutes * pointHeight)));
			}

            canvas.DrawPoints(SKPointMode.Polygon, timePoints.ToArray(), timePaint);

            #endregion
            SKPaint distancePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Blue.ToSKColor(),
                StrokeWidth = 2
            };

            biggestValue = 0;

            List<SKPoint> distancePoints = new List<SKPoint>();

            for (int i = 0; i < pointsCount; ++i)
            {
                biggestValue = viewModel.Trainings[i].Distance > biggestValue ? viewModel.Trainings[i].Distance : biggestValue;
                pointHeight = pictureHeight / biggestValue;
                //canvas.DrawText(viewModel.Trainings[i].DisplayDate, i * pointWidth, pictureHeight - 10, textPaint);
                canvas.DrawText(viewModel.Trainings[i].Distance.ToString(), 15, pictureHeight - 15 - (int)(viewModel.Trainings[i].Distance * pointHeight), textPaint);

                distancePoints.Add(new SKPoint(i * pointWidth, pictureHeight - 15 - (int)(viewModel.Trainings[i].Distance * pointHeight)));
            }

            canvas.DrawPoints(SKPointMode.Polygon, distancePoints.ToArray(), distancePaint);
            #region draw distance

            #endregion

            SKPaint axesPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Aqua.ToSKColor(),
                StrokeWidth = 2
            };

            canvas.DrawLine(5, pictureHeight - 5, pictureWidth, pictureHeight - 5, axesPaint);
			canvas.DrawLine(5, pictureHeight - 5, 5, 0, axesPaint);
		}
    }
}
