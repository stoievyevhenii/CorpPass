using CorpPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        SettingsPageViewModel _viewModel;
        
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new SettingsPageViewModel();
        }
    }
}