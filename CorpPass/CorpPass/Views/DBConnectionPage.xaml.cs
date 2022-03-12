using CorpPass.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DBConnectionPage : ContentPage
    {
        DBConnectionViewModel _viewModel;

        public DBConnectionPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new DBConnectionViewModel();
        }

        private async void GoBack(object sender, SwipedEventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}