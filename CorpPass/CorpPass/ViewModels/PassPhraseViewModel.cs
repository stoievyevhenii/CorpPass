using CorpPass.Services;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    internal class PassPhraseViewModel : BaseViewModel
    {
        private string passPhrase;

        public PassPhraseViewModel()
        {
            PassPhrase = GetPassPhrase();
            SavePassPhraseCommand = new Command(SavePassPhrase, ValidateSave);
            PropertyChanged += (_, __) => SavePassPhraseCommand.ChangeCanExecute();
        }

        public string PassPhrase
        {
            get => passPhrase;
            set => SetProperty(ref passPhrase, value);
        }

        public Command SavePassPhraseCommand { get; }

        private string GetPassPhrase()
        {
            var preferencesSecurity = new PreferencesSecurity();
            return preferencesSecurity.GetSecureData(PreferencesKeys.PassPhrase).Result;
        }

        private async void SavePassPhrase()
        {
            try
            {
                var preferencesSecurity = new PreferencesSecurity();
                preferencesSecurity.SetSecureData(PreferencesKeys.PassPhrase, PassPhrase);

                await Shell.Current.GoToAsync("..");
            }
            catch { }
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(passPhrase);
        }
    }
}