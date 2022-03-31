using CorpPass.Controls;
using CorpPass.Models;
using CorpPass.Services;
using CorpPass.ViewModels;
using System;
using System.Security.Cryptography;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace CorpPass.Views
{
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();

            SelectedIcon.Text = "icon_lock_color";
        }

        public Item Item { get; set; }

        private async void CloseSheet(object sender, System.EventArgs e)
        {
            await Sheet.CloseSheet();
        }

        private void FolderPicker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedFolder = FolderPicker.SelectedItem.ToString();
            SelectedFolder.Text = selectedFolder;
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

        private void OpenPicker(object sender, System.EventArgs e)
        {
            FolderPicker.Focus();
        }

        private async void OpenSheet(object sender, System.EventArgs e)
        {
            await Sheet.OpenSheet();
        }

        public void ShowPassword(object sender, EventArgs e)
        {
            if (PasswordField.IsPassword)
            {
                ShowPasswordButton.Source = "icon_hide";
                PasswordField.IsPassword = false;
            }
            else
            {
                ShowPasswordButton.Source = "icon_show";
                PasswordField.IsPassword = true;
            }
        }

        private async void GeneratePassword(object sender, EventArgs e)
        {
            var selectedCount = PasswordTotalLength.Text;
            byte[] rgb = new byte[int.Parse(selectedCount)];
            RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider();
            rngCrypt.GetBytes(rgb);

            PasswordField.Text = Convert.ToBase64String(rgb);

            await Sheet.CloseSheet();
        }
    }
}