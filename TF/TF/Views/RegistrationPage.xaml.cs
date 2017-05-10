﻿using System;
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
#warning must be implemented!
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
            CreateButton.BackgroundColor = fieldsAreValid ? Color.Green : Color.Gray;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > 50)
                (sender as Entry).Text = e.OldTextValue;
        }
    }
}
