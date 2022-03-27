using CorpPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.CommunityToolkit.Extensions;
using System;
using System.Diagnostics;

namespace CorpPass.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }

        public async void CopyToClipboard(object sender, System.EventArgs e)
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
        public void ShowPassword(object sender, System.EventArgs e)
        {
            var passwordEntry = sender as Entry;
            passwordEntry.IsPassword = !passwordEntry.IsPassword;
        }
        public async void GoBack(object sender, SwipedEventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
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
    }
}