using CorpPass.Views;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        public Command RedirectToSettingsPage { get; }

        public MenuPageViewModel()
        {
            RedirectToSettingsPage = new Command(OpenSettings);
        }

        private async void OpenSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }
    }
}


