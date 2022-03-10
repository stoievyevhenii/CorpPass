using CorpPass.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CorpPass.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}