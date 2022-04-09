using CorpPass.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutFooter : Grid
    {
        public FlyoutFooter()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //((AppShell)Shell.Current).Items[0] = new ShellContent { Content = new SettingsPage() };
            //((AppShell)Shell.Current).Items.Remove(Shell.Current.Items[0]);

            await Shell.Current.GoToAsync(nameof(SettingsPage));
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}