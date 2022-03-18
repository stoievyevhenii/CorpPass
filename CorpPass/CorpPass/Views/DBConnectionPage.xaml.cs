using CorpPass.Services;
using CorpPass.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DBConnectionPage : ContentPage
    {
        DBConnectionViewModel _viewModel;

        public DBConnectionPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new DBConnectionViewModel();
            CopyToDBSwitch.IsToggled = Preferences.Get(PreferencesKeys.SaveToDB, false);

            SetItemsVisible();
        }

        private async void GoBack(object sender, SwipedEventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            SetItemsVisible();
        }

        private void SetItemsVisible()
        {
            if (!CopyToDBSwitch.IsToggled)
            {
                Preferences.Set(PreferencesKeys.SaveToDB, false);
                SaveButton.IsVisible = false;
                Fields.IsVisible = false;
                EmptyState.IsVisible = true;
            }
            else
            {
                Preferences.Set(PreferencesKeys.SaveToDB, true);
                SaveButton.IsVisible = true;
                Fields.IsVisible = true;
                EmptyState.IsVisible = false;
            }
        }
    }
}