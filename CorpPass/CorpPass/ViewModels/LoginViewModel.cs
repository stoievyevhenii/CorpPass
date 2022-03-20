using CorpPass.Services;
using CorpPass.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        bool fingerprinstIsAvailable = false;
        public bool FingerprinstIsAvailable
        {
            get { return fingerprinstIsAvailable; }
            set { SetProperty(ref fingerprinstIsAvailable, value); }
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            FingerprinstIsAvailable = Task.Run(async () => await FingerrintChecker.CheckFingerprintAvailibility()).Result;
        }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
