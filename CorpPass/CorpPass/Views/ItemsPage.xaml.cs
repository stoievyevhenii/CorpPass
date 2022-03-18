using CorpPass.Services;
using CorpPass.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;

namespace CorpPass.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        ImageButton _imageButton;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
            _imageButton = FloatAddButton;           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void GoToFirstElement(object sender, EventArgs e)
        {
            ItemsListView.ScrollTo(0, 0, animate: false);
        }
        private void EditPage(object sender, EventArgs args)
        {

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
        private async void DeleteItem(object sender, EventArgs e)
        {
            var actions = new SnackBarActionOptions
            {
                Action = () => DisplayAlert("Alert", "Clicked!", "OK"),
                Text = "Delete"
            };

            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Padding = 15,
                    Message = "Items was added to favorite list",
                },
                Actions = new[] { actions },
            };

            await this.DisplaySnackBarAsync(options);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}