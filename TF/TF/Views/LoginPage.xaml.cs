using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
			BindingContext = UserViewModel.Instance;
        }

        private async void Login_OnClicked(object sender, EventArgs e)
        {
            //var res = UserViewModel.Instance.AddNewUser();
			App.Current.MainPage = new MenuPage.MainPage ();
        }

        private void CreateAccount_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
    

