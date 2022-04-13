using System;
using System.Diagnostics;
using CorpPass.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace CorpPass.Views
{
    public partial class ItemsPage : ContentPage
    {
        private string _itemId;
        private ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
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

        private void GoToFirstElement(object sender, EventArgs e)
        {
            ItemsListView.ScrollTo(0, 0, animate: false);
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

        private void PageTabView_SelectionChanged(object sender, TabSelectionChangedEventArgs e)
        {
            var tabView = sender as TabView;
            var selectedItem = tabView.SelectedIndex;

            switch (selectedItem)
            {
                case 0:
                    Title = "All Passwords";
                    break;

                case 2:
                    Title = "Favorites";
                    break;
            }
        }
    }
}