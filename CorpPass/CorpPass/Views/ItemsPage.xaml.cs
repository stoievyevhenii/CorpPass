using CorpPass.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace CorpPass.Views
{
    public partial class ItemsPage : ContentPage
    {
        private ImageButton _imageButton;
        private string _itemId;
        private ItemsViewModel _viewModel;

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

        private void ItemsListView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.VerticalDelta > 1)
            {
                Task.WhenAll(_imageButton.ScaleTo(0, 200));
            }
            else if (e.VerticalDelta < 0 && Math.Abs(e.VerticalDelta) > 10)
            {
                Task.WhenAll(_imageButton.ScaleTo(1, 200));
            }
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
    }
}