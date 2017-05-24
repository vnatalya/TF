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

            #region draw time

            SKPaint timePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 1
            };

            List<SKPoint> timePoints = new List<SKPoint>();
			for (int i = 0; i < pointsCount; ++i)
			{
				biggestValue = viewModel.Trainings[i].Time.TotalMinutes > biggestValue ? viewModel.Trainings[i].Time.TotalMinutes : biggestValue;
				pointHeight = (pictureHeight - 15) / biggestValue;
				canvas.DrawText(viewModel.Trainings[i].DisplayDate, 30 + i * pointWidth, pictureHeight, textPaint);
				canvas.DrawText(viewModel.Trainings[i].Time.TotalMinutes.ToString(), 20, pictureHeight - (int)(viewModel.Trainings[i].Time.TotalMinutes * pointHeight), textPaint);
				
				timePoints.Add(new SKPoint(30 + i * pointWidth, pictureHeight - 15 - (int)(viewModel.Trainings[i].Time.TotalMinutes * pointHeight)));
			}

            canvas.DrawPoints(SKPointMode.Polygon, timePoints.ToArray(), timePaint);

            #endregion
            SKPaint distancePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Blue.ToSKColor(),
                StrokeWidth = 1
            };

            biggestValue = 0;

            List<SKPoint> distancePoints = new List<SKPoint>();

            for (int i = 0; i < pointsCount; ++i)
            {
                biggestValue = viewModel.Trainings[i].Distance > biggestValue ? viewModel.Trainings[i].Distance : biggestValue;
				pointHeight = (pictureHeight  - 15)/ biggestValue;
                //canvas.DrawText(viewModel.Trainings[i].DisplayDate, i * pointWidth, pictureHeight - 10, textPaint);
                canvas.DrawText(viewModel.Trainings[i].Distance.ToString(), 20, pictureHeight - (int)(viewModel.Trainings[i].Distance * pointHeight), textPaint);

                distancePoints.Add(new SKPoint(30 + i * pointWidth, pictureHeight - 15 - (int)(viewModel.Trainings[i].Distance * pointHeight)));
            }

            canvas.DrawPoints(SKPointMode.Polygon, distancePoints.ToArray(), distancePaint);
            #region draw distance

            #endregion

            SKPaint axesPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Aqua.ToSKColor(),
                StrokeWidth = 3
            };

            canvas.DrawLine(30, pictureHeight - 15, pictureWidth, pictureHeight - 15, axesPaint);
			canvas.DrawLine(30, pictureHeight - 15, 30, 0, axesPaint);
		}
    }
}
