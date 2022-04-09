using CorpPass.Services;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class NewPINViewModel : BaseViewModel
    {
        private string _pin;
        private string _pinConfirmation;

        public NewPINViewModel()
        {
            SavePIN = new Command(SaveNewPIN, ValidateSave);
            PropertyChanged += (_, __) => SavePIN.ChangeCanExecute();
        }

        public string PIN
        {
            get => _pin;
            set => SetProperty(ref _pin, value);
        }

        public string PINConfirmation
        {
            get => _pinConfirmation;
            set => SetProperty(ref _pinConfirmation, value);
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
            return !string.IsNullOrWhiteSpace(_pin) && _pin.Length == 4 && _pin == _pinConfirmation;
        }

        #endregion UTILITIES
    }
}