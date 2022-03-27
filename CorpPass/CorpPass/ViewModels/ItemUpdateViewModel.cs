using CorpPass.Models;
using CorpPass.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    internal class ItemUpdateViewModel : BaseViewModel
    {
        private readonly FolderItems _itemsFolderModel;
        private readonly GroupItems _itemsGroupModel;
        private List<string> _folderList;
        private List<string> _groupList;
        private string description;
        private string folder;
        private string group;
        private string icon;
        private string itemId;
        private string login;
        private string name;
        private string password;
        private int selectedFolder;
        private int selectedGroup;

        public ItemUpdateViewModel()
        {
            _itemsGroupModel = new GroupItems();
            _groupList = _itemsGroupModel.GetGroupsNameList();

            _itemsFolderModel = new FolderItems();
            _folderList = _itemsFolderModel.GetFoldersList();

            UpdateCommand = new Command(UpdateItem, ValidateSave);

            this.PropertyChanged +=
                (_, __) => UpdateCommand.ChangeCanExecute();
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Folder
        {
            get => folder;
            set => SetProperty(ref folder, value);
        }

        public List<string> FolderList
        {
            get => _folderList;
        }

        public string Group
        {
            get => group;
            set => SetProperty(ref group, value);
        }

        public List<string> GroupList
        {
            get => _groupList;
        }

        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        public string Id { get; set; }

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

        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public int SelectedFolder
        {
            get => selectedFolder;
            set => SetProperty(ref selectedFolder, value);
        }

        public int SelectedGroup
        {
            get => selectedGroup;
            set => SetProperty(ref selectedGroup, value);
        }

        public Command UpdateCommand { get; }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);

                Id = item.Id;
                Name = item.Name;
                Icon = item.Icon;
                Login = item.Login;
                Password = item.Password;
                Group = item.Group;
                Folder = item.Folder;
                Description = item.Description;

                SelectedGroup = _groupList.IndexOf(item.Group);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void UpdateItem()
        {
            try
            {
                var updatedItem = new Item()
                {
                    Id = Id,
                    Name = Name,
                    Icon = Icon,
                    Login = Login,
                    Password = Password,
                    Group = Group,
                    Folder = Folder,
                    Description = Description
                };

                await DataStore.UpdateItemAsync(updatedItem);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(login)
                && !string.IsNullOrWhiteSpace(password)
                && !string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(group)
                && !string.IsNullOrWhiteSpace(folder);
        }
    }
}