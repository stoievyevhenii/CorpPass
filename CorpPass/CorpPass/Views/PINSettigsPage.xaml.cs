using CorpPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PINSettigsPage : ContentPage
    {
        public PINSettigsPage()
        {
            InitializeComponent();
            BindingContext = new NewPINViewModel();
        }
    }
}