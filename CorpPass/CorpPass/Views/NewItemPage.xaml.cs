using CorpPass.Models;
using CorpPass.ViewModels;
using Xamarin.Forms;

namespace CorpPass.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }

        private async void GoBack(object sender, SwipedEventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private void GroupPicker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedGroup = GroupPicker.SelectedIndex;
            SelectedGroup.Text = selectedGroup.ToString();
        }

        private void FolderPicker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedFolder = FolderPicker.SelectedIndex;
            SelectedFolder.Text = selectedFolder.ToString();
        }
    }
}