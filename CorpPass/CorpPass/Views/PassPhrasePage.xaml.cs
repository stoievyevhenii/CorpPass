using System;
using CorpPass.Services;
using CorpPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PassPhrasePage : ContentPage
    {
        public PassPhrasePage()
        {
            InitializeComponent();
            BindingContext = new PassPhraseViewModel();
        }

        public void ShowPassword(object sender, EventArgs e)
        {
            if (NewPassPhrase.IsPassword)
            {
                ShowPasswordButton.Source = "icon_hide";
                NewPassPhrase.IsPassword = false;
            }
            else
            {
                ShowPasswordButton.Source = "icon_show";
                NewPassPhrase.IsPassword = true;
            }
        }

        private async void PasswordField_TextChanged(object sender, TextChangedEventArgs e)
        {
            var health = PasswordSecurity.CheckStrength(NewPassPhrase.Text);

            if (health <= 0.40)
            {
                PasswordStrongestBar.ProgressColor = Color.FromHex("c73434");
            }
            if (health > 0.40 && health <= 0.66)
            {
                PasswordStrongestBar.ProgressColor = Color.FromHex("f7b733");
            }
            if (health > 0.66)
            {
                PasswordStrongestBar.ProgressColor = Color.FromHex("4caf50");
            }

            await PasswordStrongestBar.ProgressTo(health, 250, Easing.Linear);
        }
    }
}