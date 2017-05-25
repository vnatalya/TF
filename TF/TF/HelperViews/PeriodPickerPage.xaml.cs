using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TF.TriathlonViewModel;

namespace TF.HelperViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeriodPickerPage : ContentPage
    {
		TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }

        public PeriodPickerPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
			viewModel.CurrentPeriod = (PeriodType)((int)viewModel.PeriodList.IndexOf((NamedItem)e.SelectedItem));
            Navigation.PopAsync();
        }
    }
}