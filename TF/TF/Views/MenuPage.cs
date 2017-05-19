using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net.Http;
using TF.Views;

namespace TF
{
	public class MenuPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		ListView listView;

		public MenuPage ()
		{
			var masterPageItems = new List<MasterPageItem> ();
			masterPageItems.Add (new MasterPageItem 
			{
				Title = "Contacts",
				IconSource = "contacts.png",
				TargetType = typeof (AboutPage)
			});

			if (UserViewModel.Instance.User.Role != User.UserRole.Private)
				masterPageItems.Add (new MasterPageItem 
				{
					Title = UserViewModel.Instance.User.Role == User.UserRole.Teacher ? StringService.Instance .MyGroups : StringService.Instance.MyFeedbacks,
					IconSource = "reminders.png",
					TargetType = UserViewModel.Instance.User.Role == User.UserRole.Teacher ? typeof (GroupsListPage) : typeof (FeedbackOverviewPage)
				});
			if (App.IsLoggedIn)
				masterPageItems.Add (new MasterPageItem {
				Title = StringService.Instance.Logout,
					IconSource = "todo.png",
					TargetType = typeof (LoginPage)
				});
			listView = new ListView {
				ItemsSource = masterPageItems,
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Title");
					imageCell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");
					return imageCell;
				}),
				VerticalOptions = LayoutOptions.FillAndExpand,
				SeparatorVisibility = SeparatorVisibility.None
			};

			Padding = new Thickness (0, 40, 0, 0);
			Icon = "hamburger.png";
			Title = "Personal Organiser";
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
				listView
			}
			};
		}

		public class MainPage : MasterDetailPage
		{
			MenuPage masterPage;

			public MainPage ()
			{
				masterPage = new MenuPage ();
				Master = masterPage;
				Detail = new NavigationPage (new AboutPage ()) { BarBackgroundColor = Color.Green };
				masterPage.ListView.ItemSelected += OnItemSelected;
			}

			void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
			{
				var item = e.SelectedItem as MasterPageItem;

				if (item == null)
					return;

				if (!(item is LoginPage)) 
				{
					Detail = new NavigationPage ((Page)Activator.CreateInstance (item.TargetType)) { BarBackgroundColor = Color.Green };
					masterPage.ListView.SelectedItem = null;
					IsPresented = false;
				} 
				else 
				{
					App.Current.MainPage = new NavigationPage (new LoginPage ()) { BackgroundColor = Color.Green };
				}
			}
		}


		public class MasterPageItem
		{
			public string Title { get; set; }
			public string IconSource { get; set; }
			public Type TargetType { get; set; }
		}
	}
}
