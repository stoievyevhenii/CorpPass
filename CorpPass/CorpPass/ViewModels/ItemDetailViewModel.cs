using CorpPass.Models;
using CorpPass.Services;
using CorpPass.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string name;
        private string icon;
        private string login;
        private string password;
        private string group;
        private string folder;
        private string description;
        private Icon iconModel;

        public string Id { get; set; }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }
        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }        
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public string Group
        {
            get => group;
            set => SetProperty(ref group, value);
        }
        public string Folder
        {
            get => folder;
            set => SetProperty(ref folder, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public Icon IconModel
        {
            get => iconModel;
            set => SetProperty(ref iconModel, value);
        }
        public Command DeleteItem { get; }
        public Command OnChangeFavoriteStatus { get; }
        public Command UpdateItem { get; }
        public List<CollectionListItem> BottomSheetItems { get; set; }

        public ItemDetailViewModel()
        {
            BottomSheetItems = new List<CollectionListItem>();
            DeleteItem = new Command(OnDeleteItem);
            UpdateItem = new Command(OnEditItem);
            OnChangeFavoriteStatus = new Command(ChangeFavoriteStatus);

            InitItemContextMenuItems();
        }

        private void InitItemContextMenuItems()
        {
            BottomSheetItems.Add(new CollectionListItem()
            {
                Icon = "icon_edit",
                Name = "Edit",
                ItemCommand = UpdateItem
            });
            BottomSheetItems.Add(new CollectionListItem()
            {
                Icon = "icon_delete",
                Name = "Delete",
                ItemCommand = DeleteItem
            });
            BottomSheetItems.Add(new CollectionListItem()
            {
                Icon = "icon_favorite",
                Name = "Change favorite status",
                ItemCommand = OnChangeFavoriteStatus
            });
        }
        private async void OnEditItem()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemUpdatePage)}?{nameof(ItemUpdateViewModel.ItemId)}={ItemId}");
        }
        public async void OnDeleteItem()
        {
            await DataStore.DeleteItemAsync(ItemId);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public async void ChangeFavoriteStatus()
        {
            var selectedItem = await DataStore.GetItemAsync(ItemId);
            selectedItem.IsFavorite = selectedItem.IsFavorite == true ? false : true;
            await DataStore.UpdateItemAsync(selectedItem);
        }
        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                
                Id = item.Id;
                Icon = item.Icon;
                IconModel = item.IconModel;
                Name = item.Name;
                Login = item.Login;
                Password = item.Password;
                Group = item.Group;
                Folder = item.Folder;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
