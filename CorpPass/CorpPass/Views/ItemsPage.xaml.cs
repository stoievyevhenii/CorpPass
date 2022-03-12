using CorpPass.Services;
using CorpPass.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CorpPass.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        Frame _imageButton;

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
            ItemsListView.ScrollTo(0, animate: false);
        }

        private void ItemsListView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.VerticalDelta > 1)
            {
                Task.WhenAll(_imageButton.TranslateTo(0, 100, easing: Easing.Linear));
            }
            else if (e.VerticalDelta < 0 && Math.Abs(e.VerticalDelta) > 10)
            {
                Task.WhenAll(_imageButton.TranslateTo(0, 0, easing: Easing.Linear));
            }
        }
    }
}