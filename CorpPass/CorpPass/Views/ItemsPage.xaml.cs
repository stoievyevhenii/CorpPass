using CorpPass.ViewModels;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CorpPass.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        
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
    }
}