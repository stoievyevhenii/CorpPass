﻿using CorpPass.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using System.Threading.Tasks;

namespace CorpPass.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        ImageButton _imageButton;
        string _itemId;

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
        private void GoToFirstElement(object sender, EventArgs e)
        {
            ItemsListView.ScrollTo(0, 0, animate: false);
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
        protected override bool OnBackButtonPressed()
        {
            return true;
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