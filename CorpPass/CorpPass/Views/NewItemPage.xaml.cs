using System;
using System.Security.Cryptography;
using CorpPass.Controls;
using CorpPass.Models;
using CorpPass.Services;
using CorpPass.ViewModels;
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

        private async void CloseSheet(object sender, EventArgs e)
        {
            await Sheet.CloseSheet();
        }

        private void FolderPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedFolder = FolderPicker.SelectedItem.ToString();
            SelectedFolder.Text = selectedFolder;
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

        private async void GoBack(object sender, SwipedEventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private void GroupPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedGroup = GroupPicker.SelectedIndex;
            SelectedGroup.Text = selectedGroup.ToString();
        }

        private async void IconPicker_Clicked(object sender, EventArgs e)
        {
            var selectedIcon = await Navigation.ShowPopupAsync(new PopControl());
            var selectedIconName = selectedIcon.ToString().Replace("File:", "").Trim();

            var iconModel = new IconsSet();
            var icon = iconModel.GetIconsSet().Find(x => x.Name == selectedIconName);

            SelectedIcon.Text = selectedIconName;
            ItemIcon.Source = icon.Name;
            IconPickerFrame.BorderColor = icon.Accent;
        }

        private void OpenPicker(object sender, EventArgs e)
        {
            FolderPicker.Focus();
        }

        private async void OpenSheet(object sender, EventArgs e)
        {
            await Sheet.OpenSheet();
        }

        private async void PasswordField_TextChanged(object sender, TextChangedEventArgs e)
        {
            var health = PasswordSecurity.CheckStrength(PasswordField.Text);

            if (health <= 0.40)
            {
                PasswordStrongestBar.ProgressColor = Color.FromHex("c73434");
            }
            if (health > 0.40 && health <= 0.66)
            {
                PasswordStrongestBar.ProgressColor = Color.FromHex("f7b733");
            }
            if (health > 0.66)
            {
                PasswordStrongestBar.ProgressColor = Color.FromHex("4caf50");
            }

            await PasswordStrongestBar.ProgressTo(health, 250, Easing.Linear);
        }
    }
}