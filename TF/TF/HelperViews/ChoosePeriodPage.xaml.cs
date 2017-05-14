using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF.HelperViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoosePeriodPage : ContentPage
    {
        TriathlonViewModel viewModel { get { return TriathlonViewModel.Instance; } }
        public ChoosePeriodPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var picker = new DatePicker();
        }
    }
}