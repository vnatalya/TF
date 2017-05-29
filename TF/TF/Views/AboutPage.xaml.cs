using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using TF.HelperViews;
using System;

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
			ChartView.WidthRequest = App.DeviceWidth * 2 - 30;
			if (viewModel.IsEditMode) 
			{
				var toolBar = new ToolbarItem { Name = viewModel.StringAddTraining };
				toolBar.Clicked += AddItem_Clicked;

				ToolbarItems.Add (toolBar);
			}
		}

        private void Button_Click(object sender, System.EventArgs e)
        {
			ContentPage filterPage =  sender == TypeButton ? new TypePickerPage(true) as ContentPage : sender == PeriodButton ? new PeriodPickerPage() as ContentPage : new ItemsPage();
			Navigation.PushAsync(filterPage);
        }

		async void AddItem_Clicked (object sender, EventArgs e)
		{
			viewModel.SetCurrentItem ();
			await Navigation.PushAsync (new NewItemPage ());
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
			canvasView.WidthRequest = ChartView.Bounds.Width  - 1;
			canvasView.HeightRequest = ChartView.Bounds.Height - 2;
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
				TextSize = 14,
				TextAlign = SKTextAlign.Center
			};

			var pointsCount = viewModel.Trainings.Count;

			var pictureWidth = info.Width;
			var pictureHeight = info.Height;

			var pointWidth = pictureWidth / pointsCount;
			double pointHeight = 1;

			double biggestValue = 0;

            #region draw distance and time

            SKPaint timePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
				TextSize = 16,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 1
            };

			SKPaint timePointPaint = new SKPaint {
				Style = SKPaintStyle.Stroke,
				Color = Color.Red.ToSKColor (),
				StrokeWidth = 4
			};


            SKPaint distancePaint = new SKPaint {
				Style = SKPaintStyle.Stroke,
				TextSize = 16,
				Color = Color.Blue.ToSKColor (),
				StrokeWidth = 1
			};

			SKPaint distancePointPaint = new SKPaint {
				Style = SKPaintStyle.Stroke,
				Color = Color.Blue.ToSKColor (),
				StrokeWidth = 2
			};

			for (int i = 0; i < pointsCount; ++i) 
			{
				biggestValue = viewModel.Trainings [i].Time.TotalHours > biggestValue ? viewModel.Trainings [i].Time.TotalHours : biggestValue;
				biggestValue = viewModel.Trainings [i].Distance > biggestValue ? viewModel.Trainings [i].Distance : biggestValue;
			}

            List<SKPoint> timePoints = new List<SKPoint>();
			List<SKPoint> distancePoints = new List<SKPoint> ();
			
			for (int i = 0; i < pointsCount; ++i) 
			{
				pointHeight = (pictureHeight - bottomOffset) / biggestValue;
				canvas.DrawText(viewModel.Trainings[i].DisplayDate, leftOffset + i * pointWidth, pictureHeight , textPaint);
				canvas.DrawText(String.Format ("{0:0.0}" ,viewModel.Trainings[i].Time.TotalHours), 20, pictureHeight - bottomOffset - (int)(viewModel.Trainings[i].Time.TotalHours * pointHeight), textPaint);
				
				timePoints.Add(new SKPoint(leftOffset + i * pointWidth, pictureHeight - bottomOffset - (int)(viewModel.Trainings[i].Time.TotalHours * pointHeight)));

				canvas.DrawText (viewModel.Trainings [i].Distance.ToString (), 20, pictureHeight - (int)(viewModel.Trainings [i].Distance * pointHeight), textPaint);

				distancePoints.Add (new SKPoint (leftOffset + i * pointWidth, pictureHeight - bottomOffset - (int)(viewModel.Trainings [i].Distance * pointHeight)));
			}

			canvas.DrawPoints (SKPointMode.Points, timePoints.ToArray (), timePointPaint);

			canvas.DrawPoints (SKPointMode.Polygon, timePoints.ToArray (), timePaint);

			if (timePoints.Count > 0)			
				canvas.DrawText (StringService.Instance.Time, timePoints [timePoints.Count - 1].X, timePoints [timePoints.Count - 1].Y, timePaint);
			
			canvas.DrawPoints (SKPointMode.Points, distancePoints.ToArray (), distancePointPaint);
			
			if (distancePoints.Count > 0)
				canvas.DrawText (StringService.Instance.Distance, distancePoints[distancePoints.Count - 1].X, distancePoints [distancePoints.Count - 1].Y, distancePaint);
			
            canvas.DrawPoints(SKPointMode.Polygon, distancePoints.ToArray(), distancePaint);

            #endregion

            SKPaint axesPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Aqua.ToSKColor(),
                StrokeWidth = 3
            };

            canvas.DrawLine(leftOffset, pictureHeight - bottomOffset, pictureWidth, pictureHeight - bottomOffset, axesPaint);
			canvas.DrawLine(leftOffset, pictureHeight - bottomOffset, leftOffset, 0, axesPaint);
		}

		int bottomOffset = 30;
		int leftOffset = 30;
    }
}
