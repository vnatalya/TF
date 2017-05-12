
using Xamarin.Forms;

namespace TF.Views
{
    public partial class AboutPage : ContentPage
    {
        TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }
        TriathlonTraining currentItem { get { return TriathlonViewModel.Instance.CurrentItem; } }

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void Button_Click(object sender, System.EventArgs e)
        {
			var filterPage = new ExpandableListViewPage(sender == TypeButton);
			await Navigation.PushModalAsync(filterPage);

        }
    }
}
