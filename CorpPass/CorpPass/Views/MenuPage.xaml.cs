using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private async void OpenDBSettings(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(DBConnectionPage));
        }

        private async void GoBack(object sender, SwipedEventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}