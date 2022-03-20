using CorpPass.Views;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        public Command RedirectToSettingsPage { get; }
        public Command RedirectToAboutPage { get; }


        public MenuPageViewModel()
        {
            RedirectToSettingsPage = new Command(OpenSettingsPage);
            RedirectToAboutPage = new Command(OpenAboutPage);
        }

        private async void OpenSettingsPage()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        private async void OpenAboutPage()
        {
            await Shell.Current.GoToAsync(nameof(AboutPage));
        }
    }
}


