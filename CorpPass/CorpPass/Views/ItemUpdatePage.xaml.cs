using CorpPass.Controls;
using CorpPass.Services;
using CorpPass.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
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

        private async void IconPicker_Clicked(object sender, System.EventArgs e)
        {
            var selectedIcon = await Navigation.ShowPopupAsync(new PopControl());
            var selectedIconName = selectedIcon.ToString().Replace("File:", "").Trim();

            var iconModel = new IconsSet();
            var icon = iconModel.GetIconsSet().Find(x => x.Name == selectedIconName);

            SelectedIcon.Text = selectedIconName;
            ItemIcon.Source = icon.Name;
            IconPickerFrame.BorderColor = icon.Accent;
        }
    }
}