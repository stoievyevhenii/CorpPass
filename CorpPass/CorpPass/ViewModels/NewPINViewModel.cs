using CorpPass.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class NewPINViewModel : BaseViewModel
    {
        private string pin;
        public string PIN
        {
            get => pin;
            set => SetProperty(ref pin, value);
        }
        public Command SavePIN { get; }

        public NewPINViewModel()
        {
            SavePIN = new Command(SaveNewPIN,ValidateSave);
            PropertyChanged += (_, __) => SavePIN.ChangeCanExecute();
        }

        private void SaveNewPIN()
        {
            Preferences.Set(PreferencesKeys.PIN, PIN);
        }

        #region UTILITIES
        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(pin) && pin.Length == 4;
        }
        #endregion
    }
}
