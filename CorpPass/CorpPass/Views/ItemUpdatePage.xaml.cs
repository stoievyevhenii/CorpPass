using CorpPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemUpdatePage : ContentPage
    {
        ItemUpdateViewModel _viewModel;
     
        public ItemUpdatePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemUpdateViewModel();
        }
    }
}