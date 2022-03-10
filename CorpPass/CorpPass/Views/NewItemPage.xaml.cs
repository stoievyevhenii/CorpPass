using CorpPass.Models;
using CorpPass.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }

        private async void GoBack(object sender, SwipedEventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}