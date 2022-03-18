using CorpPass.Views;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public Command RedirectToDBConnectPage { get; }
        public Command RedirectToPINPage { get; }


        public SettingsPageViewModel()
        {
            RedirectToDBConnectPage = new Command(OpenDBSettings);
            RedirectToPINPage = new Command(OpenPINSettings);
        }

        private async void OpenDBSettings()
        {
            await Shell.Current.GoToAsync(nameof(DBConnectionPage));
        }

        private async void OpenPINSettings()
        {
            await Shell.Current.GoToAsync(nameof(PINSettigsPage));
        }
    }
}
