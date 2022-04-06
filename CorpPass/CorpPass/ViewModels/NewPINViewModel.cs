using CorpPass.Services;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class NewPINViewModel : BaseViewModel
    {
        private string pin;

        public NewPINViewModel()
        {
            SavePIN = new Command(SaveNewPIN, ValidateSave);
            PropertyChanged += (_, __) => SavePIN.ChangeCanExecute();
        }

        public string PIN
        {
            get => pin;
            set => SetProperty(ref pin, value);
        }

        public Command SavePIN { get; }

        private async void SaveNewPIN()
        {
            try
            {
                var preferencesSecurity = new PreferencesSecurity();
                preferencesSecurity.SetSecureData(PreferencesKeys.PIN, PIN);

                await Shell.Current.GoToAsync("..");
            }
            catch { }
        }

        #region UTILITIES

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(pin) && pin.Length == 4;
        }

        #endregion UTILITIES
    }
}