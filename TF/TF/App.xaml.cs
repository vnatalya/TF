﻿using System;
using TF.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TF
{
    public partial class App : Application
    {
		public static float DeviceWidth;
		public static float DeviceHeight;
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

		public static void SetMainPage()
		{
			TriathlonViewModel.Instance.Initialize();
			if (IsLoggedIn)
			{
				Current.MainPage = //new TabbedPage {
					//Children =
					//{
					//	new NavigationPage(new ItemsPage())
					//	{
					//		Title = "Browse",
					//		Icon = Device.OnPlatform("tab_feed.png",null,null),
					//			BarBackgroundColor = Color.Green
					//	},
						new NavigationPage (new AboutPage ()) {
							Title = "About",
							Icon = Device.OnPlatform ("tab_about.png", null, null),
							BarBackgroundColor = Color.Green
							//}
						//}
				};
			}
			else
				Current.MainPage = new NavigationPage(new LoginPage()) { BarBackgroundColor = Color.White };

		}

        public static bool IsLoggedIn
        {
            get { return !string.IsNullOrWhiteSpace(_Token); }
        }

        static string _Token;
        public static string Token
        {
            get { return _Token; }
        }

        public static void SaveToken(string token)
        {
            _Token = token;
        }

        public static Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() => {
                   // NavigationPage.PopModalAsync();
                });
            }
        }
    }
}
