using CorpPass.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupItemsPage : ContentPage
    {
        private string _itemId;
        private GroupItemsViewModel _viewModel;

        public GroupItemsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new GroupItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void AddItemToFavorite(object sender, EventArgs e)
        {
            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Padding = 15,
                    Message = "Items was added to favorite list",
                }
            };

            await this.DisplaySnackBarAsync(options);
        }

        private async void OpenBottomSheet(object sender, EventArgs e)
        {
            try
            {
                var imageButton = sender as ImageButton;
                var itemId = imageButton.CommandParameter as string;

                _itemId = itemId;

                SelectedItemID.Text = _itemId;

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