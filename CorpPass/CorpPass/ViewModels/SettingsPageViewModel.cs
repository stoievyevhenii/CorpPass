using CorpPass.Views;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public Command RedirectToDBConnectPage { get; }

        public SettingsPageViewModel()
        {
            RedirectToDBConnectPage = new Command(OpenDBSettings);
        }

        private async void OpenDBSettings()
        {
            await Shell.Current.GoToAsync(nameof(DBConnectionPage));
        }
    }
}
