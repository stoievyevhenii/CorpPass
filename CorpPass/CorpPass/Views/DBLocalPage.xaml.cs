using CorpPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DBLocalPage : ContentPage
    {
        public DBLocalPage()
        {
            InitializeComponent();
            BindingContext = new DBLocalViewModel();
        }
    }
}