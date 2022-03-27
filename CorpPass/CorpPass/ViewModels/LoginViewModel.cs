using CorpPass.Services;
using CorpPass.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool fingerprinstIsAvailable = false;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            FingerprinstIsAvailable = Task.Run(async () => await FingerrintChecker.CheckFingerprintAvailibility()).Result;
        }

        public bool FingerprinstIsAvailable
        {
            get { return fingerprinstIsAvailable; }
            set { SetProperty(ref fingerprinstIsAvailable, value); }
        }

        public Command LoginCommand { get; }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}