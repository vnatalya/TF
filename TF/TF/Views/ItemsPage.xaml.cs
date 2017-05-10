using System;

using TF.Models;
using TF.ViewModels;

using Xamarin.Forms;

namespace TF.Views
{
    public partial class ItemsPage : ContentPage
    {
		TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new NewItemPage());

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        private void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert(string.Empty, StringService.Instance.DeleteAlert, null, null);
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
			viewModel.SetCurrentItem();
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Items.Count == 0)
            //	viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
