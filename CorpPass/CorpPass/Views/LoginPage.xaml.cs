using System;
using System.Threading.Tasks;
using CorpPass.Services;
using CorpPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private string _pin;
        private string _passPhrase;

        private PreferencesSecurity _prefernecesSecurity;

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = new LoginViewModel();
            _prefernecesSecurity = new PreferencesSecurity();

            _pin = _prefernecesSecurity.GetSecureData(PreferencesKeys.PIN).Result;
            _passPhrase = _prefernecesSecurity.GetSecureData(PreferencesKeys.SavePassPhrase).Result;

            CheckFirstStart();
        }

        #region HANDLERS

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

        private void FirstPINEnter(object sender, EventArgs e)
        {
            PinTabView.SelectedIndex = 2;
        }

        private void NumberButtonClicked(object sender, EventArgs e)
        {
            var clickedButton = sender as Button;
            var number = clickedButton.Text;

            PassField.Text += number;
        }

        private async void PassField_TextChanged(object sender, TextChangedEventArgs e)
        {
            await PasscodeFieldChecker();
        }

        private async void SetNewPin(object sender, EventArgs e)
        {
            try
            {
                if (NewPin.Text == RepeatNewPin.Text)
                {
                    _prefernecesSecurity.SetSecureData(PreferencesKeys.PIN, NewPin.Text);
                    PinTabView.SelectedIndex = 3;
                }
                else
                {
                    await DisplayAlert("Error!", "Entered PIN codes is different", "OK");
                }
            }
            catch
            {
                await DisplayAlert("Error!", "Can`t set new PIN", "OK");
            }
        }

        private void SetNewPassPhrase(object sender, EventArgs e)
        {
            try
            {
                var preferencesSecurity = new PreferencesSecurity();
                preferencesSecurity.SetSecureData(PreferencesKeys.SavePassPhrase, PassPhrase.Text);

                PinTabView.SelectedIndex = 0;
            }
            catch { }
        }

        private void UseFingerprintAuth(object sender, EventArgs e)
        {
            UseBiometrickAuth();
        }

        private void BackToEnterPINPage(object sender, EventArgs e)
        {
            PinTabView.SelectedIndex = 1;
        }

        #endregion HANDLERS

        #region UTILS

        private void CheckFirstStart()
        {
            if (string.IsNullOrEmpty(_pin))
            {
                PinTabView.SelectedIndex = 1;
            }
            if (string.IsNullOrEmpty(_passPhrase))
            {
                PinTabView.SelectedIndex = 3;
            }
        }

        private async Task PasscodeFieldChecker()
        {
            var passcodeField = PassField;
            var passcodeText = passcodeField.Text;

            if (passcodeText.Length == 4)
            {
                _pin = _prefernecesSecurity.GetSecureData(PreferencesKeys.PIN).Result;
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

        private async void UseBiometrickAuth()
        {
            var scannerIsAvailable = await FingerprintChecker.CheckFingerprintAvailibility();

            if (scannerIsAvailable)
            {
                var fingerprintScanResult = await FingerprintChecker.UseFingerprint();
                var authed = fingerprintScanResult[FingerprintScanKeys.Authed];

                if (authed)
                {
                    UserWasAuth();
                }
            }
        }

        private void UserWasAuth()
        {
            NumericPad.IsVisible = false;
            LoadIndicator.IsVisible = true;

            ((AppShell)Shell.Current).Items[0] = new ShellContent { Content = new ItemsPage() };
            ((AppShell)Shell.Current).Items.Remove(Shell.Current.Items[0]);
        }

        #endregion UTILS
    }
}