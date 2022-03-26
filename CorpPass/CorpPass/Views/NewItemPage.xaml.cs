﻿using CorpPass.Controls;
using CorpPass.Models;
using CorpPass.Services;
using CorpPass.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
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

            SelectedIcon.Text = "icon_lock_color";
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
            var selectedFolder = FolderPicker.SelectedItem.ToString();
            SelectedFolder.Text = selectedFolder;
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
    }
}