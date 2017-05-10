
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

        private void TimeButton_Click(object sender, System.EventArgs e)
        {

        }
    }
}
