using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
          //  BindingContext = new ContentPageViewModel();
        }
        private void LogIn_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

		/// <summary>
		/// Is called when user click "Create my account"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Create_OnClicked(object sender, EventArgs e)
		{
			App.Current.MainPage = /*new TabbedPage
			{
				Children =
				{
					new NavigationPage(new ItemsPage())
					{
						Title = "Browse",
						Icon = Device.OnPlatform("tab_feed.png",null,null)
					},*/
					new NavigationPage(new AboutPage())
					{
						Title = "About",
						Icon = Device.OnPlatform("tab_about.png",null,null)
					//},
				//}
			};
		}

        /// <summary>
        /// Is called every time some of the entry fields becomes unfocused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Completed(object sender, FocusEventArgs e)
        {
#warning Some validation must be changed (e.g. email - must be entered valid email)!!!
            if (sender == PasswordConfirm)
            {
                PasswordConfirmLabel.IsVisible = string.IsNullOrWhiteSpace(PasswordConfirm.Text);
            }
            if (sender == Password)
            {
                PasswordLabel.IsVisible = string.IsNullOrWhiteSpace(Password.Text);
            }
            if (sender == Name)
            {
                NameLabel.IsVisible = string.IsNullOrWhiteSpace(Name.Text);
            }
            if (sender == Email)
            {
                EmailLabel.IsVisible = string.IsNullOrWhiteSpace(Email.Text);
			}

            bool fieldsAreValid = !string.IsNullOrEmpty(PasswordConfirm.Text) && !string.IsNullOrEmpty(Password.Text) && !string.IsNullOrEmpty(Email.Text) && !string.IsNullOrEmpty(Name.Text);
            CreateButton.IsEnabled = fieldsAreValid;
			CreateButton.BackgroundColor = fieldsAreValid ? Color.Green : Color.White;
			CreateButton.TextColor = fieldsAreValid ? Color.White : Color.Gray;
        }

		void RadioButton_Clicked (object sender, System.EventArgs e)
		{
			PrivateButton.Image = "radiobutton_unselected_icon";
			StudentButton.Image = "radiobutton_unselected_icon";
			TeacherButton.Image = "radiobutton_unselected_icon";

			(sender as Button).Image = "radiobutton_selected_icon";

			GroupLayout.IsVisible = sender == StudentButton;
		}

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > 50)
                (sender as Entry).Text = e.OldTextValue;
        }
    }
}
