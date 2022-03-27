using CorpPass.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsPage : ContentPage
    {
        public GroupsPage()
        {
            BindingContext = new GroupsViewModel();

            InitializeComponent();
        }
    }
}