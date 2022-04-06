using System;
using System.Diagnostics;
using CorpPass.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CorpPass.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }

        public void ChangePasswordVisibility(object sender, EventArgs e)
        {
            if (PasswordField.IsPassword)
            {
                ShowPasswordButton.Source = "icon_hide";
                PasswordField.IsPassword = false;
            }
            else
            {
                ShowPasswordButton.Source = "icon_show";
                PasswordField.IsPassword = true;
            }
        }

        public async void CopyToClipboard(object sender, EventArgs e)
        {
            string message;
            var button = sender as ImageButton;
            var param = button.CommandParameter as string;

            try
            {
                await Clipboard.SetTextAsync(param);
                message = "Copied!";
            }
            catch
            {
                message = "Something went wrong!";
            }

            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Padding = 15,
                    Message = message,
                }
            };

            await this.DisplaySnackBarAsync(options);
        }

        public async void GoBack(object sender, SwipedEventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void CloseBottomSheet(object sender, EventArgs e)
        {
            try
            {
                await Sheet.CloseSheet();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void OpenBottomSheet(object sender, EventArgs e)
        {
            try
            {
                await Sheet.OpenSheet();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}