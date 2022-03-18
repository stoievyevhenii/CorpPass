using CorpPass.Services;
using CorpPass.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private string _pin;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();

            _pin = Preferences.Get(PreferencesKeys.PIN, "");
            CheckFirstStart();
        }

        #region HANDLERS
        private void NumberButtonClicked(object sender, EventArgs e)
        {
            var clickedButton = sender as Button;
            var number = clickedButton.Text;

            PassField.Text += number;
        }
        private void BackspaceButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var enteredText = PassField.Text;
                var backspaceText = enteredText.Remove(enteredText.Length - 1);
                PassField.Text = backspaceText.ToString();
            }
            catch { }
        }
        private async void PassField_TextChanged(object sender, TextChangedEventArgs e)
        {
            await PasscodeFieldChecker();
        }
        private async void UseFingerprintAuth(object sender, EventArgs e)
        {
            var scannerIsAvailable = await FingerrintChecker.CheckFingerprintAvailibility();

            if (scannerIsAvailable)
            {
                var fingerprintScanResult = await FingerrintChecker.UseFingerprint();
                var authed = fingerprintScanResult[FingerprintScanKeys.Authed];

                if (authed)
                {
                    UserWasAuth();
                }

            }
            else
            {
                await DisplayAlert("Error", "Biometric scanner was not found!", "OK");
            }
        }
        private async void SetNewPin(object sender, EventArgs e)
        {
            try
            {
                Preferences.Set(PreferencesKeys.PIN, NewPin.Text);
                PinTabView.SelectedIndex = 0;
            }
            catch
            {
                await DisplayAlert("Error!", "Can`t set new PIN", "OK");
            }
        }
        #endregion

        #region UTILS
        private void CheckFirstStart()
        {
            if (string.IsNullOrEmpty(_pin))
            {
                PinTabView.SelectedIndex = 1;
            }
        }
        private async Task PasscodeFieldChecker()
        {
            var passcodeField = PassField;
            var passcodeText = passcodeField.Text;

            if (passcodeText.Length == 4)
            {
                _pin = Preferences.Get(PreferencesKeys.PIN, "");
                if (passcodeText == _pin)
                {
                    await Task.Delay(100);
                    UserWasAuth();
                }
                else
                {
                    PassField.TextColor = Color.FromHex("#c73434");
                    uint timeout = 50;

                    await PassField.TranslateTo(-15, 0, timeout);
                    await PassField.TranslateTo(15, 0, timeout);
                    await PassField.TranslateTo(-10, 0, timeout);
                    await PassField.TranslateTo(10, 0, timeout);
                    await PassField.TranslateTo(-5, 0, timeout);
                    await PassField.TranslateTo(5, 0, timeout);

                    PassField.TranslationX = 0;
                    PassField.Text = "";
                    PassField.TextColor = Color.White;
                }
            }
        }
        private async void UserWasAuth()
        {
            NumericPad.IsVisible = false;
            LoadIndicator.IsVisible = true;

            await Shell.Current.Navigation.PushAsync(new ItemsPage());
        }
        #endregion
    }
}